using ServiceCenter.Application.DTO.Schedule;
using ServiceCenter.Application.Helpers;
using ServiceCenter.Application.Interfaces;
using ServiceCenter.Application.Mappers;
using ServiceCenter.Domain.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ServiceCenter.Application.Services;

/// <summary>
/// Сервис для работы с графиком работы
/// Реализует <see cref="IScheduleService"/>
/// </summary>
public class ScheduleService(IScheduleRepository repository, IScheduleExceptionRepository exceptionRepository) : IScheduleService
{
    /// <inheritdoc />
    public async Task CreateAsync(ScheduleDto dto)
    {
        await repository.AddAsync(ScheduleMapper.ToEntity(dto));
    }

    public async Task DeleteAsync(Guid id)
    {
        await repository.DeleteAsync(id);
    }

    public async Task<IEnumerable<ScheduleDayDto>> GetScheduleByEmployee(Guid employeeId, DateOnly startDate, DateOnly endDate)
    {
        var schedules = (await repository.GetAllByIntervalAsync(employeeId, startDate, endDate)).OrderBy(x => x.EffectiveFrom).ToList();
        var exceptions = (await exceptionRepository.GetAllByIntervalAsync(employeeId, startDate, endDate)).OrderBy(x => x.EffectiveFrom).ToList();

        if (!schedules.Any())
            return Enumerable.Empty<ScheduleDayDto>();

        var intervalStart = startDate < schedules.FirstOrDefault()?.EffectiveFrom ? schedules.FirstOrDefault()?.EffectiveFrom ?? startDate : startDate;
        var exceptionDates = exceptions
        .SelectMany(ex =>
        {
            if (ex.EffectiveTo != default && ex.EffectiveTo >= ex.EffectiveFrom)
            {
                var days = ex.EffectiveTo.DayNumber - ex.EffectiveFrom.DayNumber + 1;
                return Enumerable.Range(0, days).Select(offset => ex.EffectiveFrom.AddDays(offset));
            }
            return new[] { ex.EffectiveFrom };
        })
        .ToHashSet();
        //TODO: это демонстрация ради демонстрации, переделать
        return Enumerable.Range(0, (endDate.DayNumber - startDate.DayNumber) + 1)
            .Select(x => intervalStart.AddDays(x))
            .Where(date => date <= endDate)
            //.Where(date => !exceptionDates.Contains(date))
        .Select(date =>
        {
            var schedule = schedules.LastOrDefault(x => x.EffectiveFrom <= date && (x.EffectiveTo >= date || x.EffectiveTo == null));
            var workDays = schedule.Pattern.Split('/').Select(x => int.Parse(x)).ToArray();
            return new ScheduleDayDto
            (
                exceptionDates.Contains(date)?"Исключение":ScheduleDaysCalculator.GetDayType(
                date,
                schedule.EffectiveFrom,
                workDays[0],
                workDays[1]
            ),
                //date.DayOfWeek >= DayOfWeek.Monday && date.DayOfWeek <= DayOfWeek.Friday ? "Рабочий" : "Выходной",
                schedule?.StartTime ?? TimeOnly.MinValue,
                schedule?.EndTime ?? TimeOnly.MinValue,
                date
            );
        }).ToList();
    }

    /// <inheritdoc />
    public Task<ScheduleDto> UpdateAsync(ScheduleDto dto)
    {
        throw new NotImplementedException();
    }

}
