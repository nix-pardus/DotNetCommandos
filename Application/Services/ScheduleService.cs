using ServiceCenter.Application.DTO.Schedule;
using ServiceCenter.Application.Interfaces;
using ServiceCenter.Application.Mappers;
using ServiceCenter.Domain.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ServiceCenter.Application.Services;

/// <summary>
/// Сервис для работы с графиком работы
/// Реализует <see cref="IScheduleService"/>
/// </summary>
public class ScheduleService(IScheduleRepository repository) : IScheduleService
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
        var schedules = (await repository.GetAllByIntervalAsync(employeeId, startDate, endDate)).OrderBy(x=>x.EffectiveFrom);
        //TODO: это демонстрация ради демонстрации, переделать
        return Enumerable.Range(0, (endDate.DayNumber-startDate.DayNumber)+1)
            .Select(x=>startDate.AddDays(x))
        .Select(date =>
        {
                var schedule = schedules.LastOrDefault(x=>x.EffectiveFrom <= date && (x.EffectiveFrom>=date || x.EffectiveFrom == null));
            return new ScheduleDayDto
            (
                date.DayOfWeek >= DayOfWeek.Monday && date.DayOfWeek <= DayOfWeek.Friday ? "Рабочий" : "Выходной",
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
