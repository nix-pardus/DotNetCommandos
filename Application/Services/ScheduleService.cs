using ServiceCenter.Application.DTO.Requests;
using ServiceCenter.Application.DTO.Responses;
using ServiceCenter.Application.DTO.Shared;
using ServiceCenter.Application.Helpers;
using ServiceCenter.Application.Interfaces;
using ServiceCenter.Application.Mappers;
using ServiceCenter.Domain.Interfaces;

namespace ServiceCenter.Application.Services;

/// <summary>
/// Сервис для работы с графиком работы
/// Реализует <see cref="IScheduleService"/>
/// </summary>
public class ScheduleService(IScheduleRepository repository, IScheduleExceptionRepository exceptionRepository, IEmployeeRepository employeeRepository, ICurrentUserService currentUserService) : IScheduleService
{
    /// <inheritdoc />
    public async Task CreateAsync(ScheduleCreateRequest dto)
    {
        var entity = ScheduleMapper.ToEntity(dto);
        entity.CreatedById = currentUserService.UserId ?? Guid.Empty;
        entity.CreatedDate = DateTime.UtcNow;
        await repository.AddAsync(entity);
    }

    public async Task DeleteAsync(Guid id)
    {
        await repository.DeleteAsync(id);
    }

    public async Task<IDictionary<EmployeeMinimalResponse, IEnumerable<ScheduleDayDto>>> GetSchedule(DateOnly startDate, DateOnly endDate)
    {
        var schedules = (await repository.GetAllByIntervalAsync(startDate, endDate)).OrderBy(x => x.EffectiveFrom).ToList();
        var exceptions = (await exceptionRepository.GetAllByIntervalAsync(startDate, endDate)).OrderBy(x => x.EffectiveFrom).ToList();
        var employeeIds = schedules
               .Select(s => s.EmployeeId)
               .Concat(exceptions.Select(e => e.EmployeeId))
               .Distinct()
               .ToList();
        var employees = await employeeRepository.GetByFiltersPagedAsync(
            filterConditions: null,
            logicalOperator: null,
            pageNumber: 1,
            pageSize: employeeIds.Count
        );
        var employeesDict = employees.Items.ToDictionary(e => e.Id);
        var result = new Dictionary<EmployeeMinimalResponse, IEnumerable<ScheduleDayDto>>();
        foreach (var employeeId in employeeIds)
        {
            if (!employeesDict.TryGetValue(employeeId, out var employee))
                continue;

            var employeeSchedules = schedules
                .Where(x => x.EmployeeId == employeeId)
                .OrderBy(x => x.EffectiveFrom)
                .ToList();

            var employeeExceptions = exceptions
                .Where(x => x.EmployeeId == employeeId)
                .OrderBy(x => x.EffectiveFrom)
                .ToList();

            if (!employeeSchedules.Any())
                continue;

            var scheduleDays = CalculateEmployeeSchedule.Calculate(
                employeeSchedules,
                employeeExceptions,
                startDate,
                endDate
            );

            var employeeDto = EmployeeMapper.ToMinimalDto(employee);
            result[employeeDto] = scheduleDays;
        }

        return result;
    }

    public async Task<IEnumerable<ScheduleDayDto>> GetScheduleByEmployee(Guid employeeId, DateOnly startDate, DateOnly endDate)
    {
        var schedules = (await repository.GetAllByIntervalAsync(employeeId, startDate, endDate)).OrderBy(x => x.EffectiveFrom).ToList();
        var exceptions = (await exceptionRepository.GetAllByIntervalAsync(employeeId, startDate, endDate)).OrderBy(x => x.EffectiveFrom).ToList();

        return CalculateEmployeeSchedule.Calculate(
                schedules,
                exceptions,
                startDate,
                endDate
            );
    }

    /// <inheritdoc />
    public Task<ScheduleFullResponse> UpdateAsync(ScheduleUpdateRequest dto)
    {
        throw new NotImplementedException();
    }

}
