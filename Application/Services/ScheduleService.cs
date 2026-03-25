using Microsoft.Extensions.Caching.Memory;
using ServiceCenter.Application.DTO.Requests;
using ServiceCenter.Application.DTO.Responses;
using ServiceCenter.Application.DTO.Shared;
using ServiceCenter.Application.Helpers;
using ServiceCenter.Application.Interfaces;
using ServiceCenter.Application.Mappers;
using ServiceCenter.Domain.Entities;
using ServiceCenter.Domain.Interfaces;
using ServiceCenter.Domain.ValueObjects.Enums;
using System.Collections.Concurrent;
using System.Linq;

namespace ServiceCenter.Application.Services;

/// <summary>
/// Сервис для работы с графиком работы
/// Реализует <see cref="IScheduleService"/>
/// </summary>
public class ScheduleService(
    IScheduleRepository repository,
    IScheduleExceptionRepository exceptionRepository,
    IEmployeeRepository employeeRepository,
    ICurrentUserService currentUserService,
    IMemoryCache memoryCache) : IScheduleService
{
    /// <inheritdoc />
    /// 
    private readonly ReaderWriterLockSlim _cacheLock = new();
    private readonly ConcurrentDictionary<string, SemaphoreSlim> _keyLocks = new();

    public async Task CreateAsync(ScheduleCreateRequest dto)
    {
        var entity = ScheduleMapper.ToEntity(dto);
        entity.CreatedById = currentUserService.UserId ?? Guid.Empty;
        entity.CreatedDate = DateTime.UtcNow;
        await repository.AddAsync(entity);
        //чистим кеш
        InvalidateCacheForEmployee(dto.EmployeeId);
    }

    public async Task DeleteAsync(Guid id)
    {
        await repository.DeleteAsync(id);
        //истим кеш
        InvalidateScheduleCache();
    }

    public async Task<IDictionary<EmployeeMinimalResponse, IEnumerable<ScheduleDayDto>>> GetSchedule(DateOnly startDate, DateOnly endDate)
    {
        //пусть будет так - да поправит меня ментор!
        var cacheKey = $"Schedule_All_{startDate:yyyyMMdd}_{endDate:yyyyMMdd}";

        // смотрим в кеше
        _cacheLock.EnterReadLock();
        try
        {
            if (memoryCache.TryGetValue(cacheKey, out IDictionary<EmployeeMinimalResponse, IEnumerable<ScheduleDayDto>> cachedResult))
            {
                return cachedResult;
            }
        }
        finally
        {
            _cacheLock.ExitReadLock();
        }

        // Блокировка для конкретного ключа
        var keyLock = _keyLocks.GetOrAdd(cacheKey, _ => new SemaphoreSlim(1, 1));
        await keyLock.WaitAsync();
        try
        {
            if (memoryCache.TryGetValue(cacheKey, out IDictionary<EmployeeMinimalResponse, IEnumerable<ScheduleDayDto>> doubleCheckResult))
            {
                return doubleCheckResult;
            }

            var result = await GetScheduleInternal(startDate, endDate);

            _cacheLock.EnterWriteLock();
            try
            {
                memoryCache.Set(cacheKey, result, TimeSpan.FromMinutes(5));
            }
            finally
            {
                _cacheLock.ExitWriteLock();
            }

            return result;
        }
        finally
        {
            keyLock.Release();
            _keyLocks.TryRemove(cacheKey, out _);
        }
    }

    private async Task<IDictionary<EmployeeMinimalResponse, IEnumerable<ScheduleDayDto>>> GetScheduleInternal(
        DateOnly startDate, DateOnly endDate)
    {
        var schedules = await repository.GetAllByIntervalAsync(startDate, endDate);
        var exceptions = await exceptionRepository.GetAllByIntervalAsync(startDate, endDate);

        var schedulesByEmployee = schedules
            .OrderBy(x => x.EffectiveFrom)
            .ToLookup(x => x.EmployeeId);
        var exceptionsByEmployee = exceptions
            .OrderBy(x => x.EffectiveFrom)
            .ToLookup(x => x.EmployeeId);

        var employeeIds = schedulesByEmployee.Select(g => g.Key)
            .Concat(exceptionsByEmployee.Select(g => g.Key))
            .Distinct()
            .ToList();

        if (!employeeIds.Any())
            return new Dictionary<EmployeeMinimalResponse, IEnumerable<ScheduleDayDto>>();

        var employees = await employeeRepository.GetByFiltersPagedAsync(
            filterConditions: null,
            logicalOperator: null,
            pageNumber: 1,
            pageSize: employeeIds.Count
        );

        var result = new Dictionary<EmployeeMinimalResponse, IEnumerable<ScheduleDayDto>>();
        foreach (var employeeId in employeeIds)
        {
            var employee = employees.Items.FirstOrDefault(e => e.Id == employeeId);
            if (employee == null) continue;

            var employeeSchedules = schedulesByEmployee[employeeId].ToList();
            if (!employeeSchedules.Any()) continue;

            var employeeExceptions = exceptionsByEmployee[employeeId].ToList();

            var scheduleDays = GetScheduleDaysIterator(
                employeeSchedules,
                employeeExceptions,
                startDate,
                endDate
            ).ToList();

            result[EmployeeMapper.ToMinimalDto(employee)] = scheduleDays;
        }

        return result;
    }

    private IEnumerable<ScheduleDayDto> GetScheduleDaysIterator(
        List<Schedule> schedules,
        List<ScheduleException> exceptions,
        DateOnly startDate,
        DateOnly endDate)
    {
        if (!schedules.Any())
            yield break;

        var exceptionTypes = exceptions
            .SelectMany(ex =>
            {
                if (ex.EffectiveTo != default && ex.EffectiveTo >= ex.EffectiveFrom)
                {
                    var days = ex.EffectiveTo.DayNumber - ex.EffectiveFrom.DayNumber + 1;
                    return Enumerable.Range(0, days).Select(offset => (ex.EffectiveFrom.AddDays(offset), ex.ExceptionType));
                }
                return new[] { (ex.EffectiveFrom, ex.ExceptionType) };
            })
            .ToDictionary(x => x.Item1, x => x.Item2);

        var exceptionDates = exceptionTypes.Keys.ToHashSet();

        for (var date = startDate; date <= endDate; date = date.AddDays(1))
        {
            var schedule = schedules.LastOrDefault(x =>
                x.EffectiveFrom <= date &&
                (x.EffectiveTo >= date || x.EffectiveTo == null));

            if (schedule == null)
            {
                yield return new ScheduleDayDto(
                    "Нет расписания",
                    TimeOnly.MinValue,
                    TimeOnly.MinValue,
                    date);
                continue;
            }

            var workDays = schedule.Pattern.Split('/').Select(int.Parse).ToArray();
            string dayType;
            if (exceptionDates.Contains(date))
            {
                var exType = exceptionTypes[date];
                dayType = exType switch
                {
                    ScheduleExceptionType.Vacation => "Отпуск",
                    ScheduleExceptionType.Sick => "Больничный",
                    _ => "Исключение из расписания"
                };
            }
            else
            {
                dayType = ScheduleDaysCalculator.GetDayType(date, schedule.EffectiveFrom, workDays[0], workDays[1]);
            }

            yield return new ScheduleDayDto(
                dayType,
                schedule.StartTime,
                schedule.EndTime,
                date
            );
        }
    }


    public async Task<IEnumerable<ScheduleDayDto>> GetScheduleByEmployee(
        Guid employeeId, DateOnly startDate, DateOnly endDate)
    {
        var cacheKey = $"Schedule_Employee_{employeeId}_{startDate:yyyyMMdd}_{endDate:yyyyMMdd}";

        if (memoryCache.TryGetValue(cacheKey, out IEnumerable<ScheduleDayDto> cachedResult))
        {
            return cachedResult;
        }

        var schedules = await repository.GetAllByIntervalAsync(startDate, endDate, employeeId);
        var exceptions = await exceptionRepository.GetAllByIntervalAsync(startDate, endDate, employeeId);

        var orderedSchedules = schedules.OrderBy(x => x.EffectiveFrom).ToList();
        var orderedExceptions = exceptions.OrderBy(x => x.EffectiveFrom).ToList();

        var result = GetScheduleDaysIterator(
            orderedSchedules,
            orderedExceptions,
            startDate,
            endDate
        ).ToList();

        memoryCache.Set(cacheKey, result, TimeSpan.FromMinutes(5));

        AddKeyToEmployeeCacheList(employeeId, cacheKey);

        return result;
    }

    private void AddKeyToEmployeeCacheList(Guid employeeId, string cacheKey)
    {
        var listKey = $"EmployeeCacheKeys_{employeeId}";
        var keys = memoryCache.GetOrCreate(listKey, entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1);
            return new HashSet<string>();
        });

        keys.Add(cacheKey);
        memoryCache.Set(listKey, keys, TimeSpan.FromHours(1));
    }

    /// <inheritdoc />
    public async Task<ScheduleFullResponse> UpdateAsync(ScheduleUpdateRequest dto)
    {
        var entity = ScheduleMapper.ToEntity(dto);
        await repository.UpdateAsync(entity);
        InvalidateCacheForEmployee(dto.EmployeeId);
        return ScheduleMapper.ToResponse(entity);

    }

    // чистим кеш от улючей
    private void InvalidateScheduleCache()
    {
        _cacheLock.EnterWriteLock();
        try
        {
            var keysToRemove = new List<string>();
            foreach (var key in _keyLocks.Keys)
            {
                memoryCache.Remove(key);
            }
        }
        finally
        {
            _cacheLock.ExitWriteLock();
        }
    }

    public void InvalidateCacheForEmployee(Guid employeeId)
    {
        var listKey = $"EmployeeCacheKeys_{employeeId}";
        var keys = memoryCache.Get<HashSet<string>>(listKey);
        if (keys != null)
        {
            foreach (var key in keys)
            {
                memoryCache.Remove(key);
            }
            memoryCache.Remove(listKey);
        }
    }

    public async Task<IEnumerable<ScheduleFullResponse>> GetSchedulesByEmployeeAsync(Guid employeeId)
    {
        var schedules = await repository.GetAllByIntervalAsync(DateOnly.MinValue, DateOnly.MaxValue, employeeId);

        return schedules.Select(ScheduleMapper.ToResponse);
    }
}
