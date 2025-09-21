using ServiceCenter.Application.DTO.Employee;
using ServiceCenter.Application.DTO.Schedule;

namespace ServiceCenter.Application.Interfaces;

/// <summary>
/// Сервис для работы с графиком работы
/// </summary>

public interface IScheduleService
{
    //TODO: пока так, но в итоге надо сделать отденьные dto
    Task CreateAsync(ScheduleDto dto);
    Task<ScheduleDto> UpdateAsync(ScheduleDto dto);
    /// <summary>
    /// Удаление графика работы по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор графика</param>
    Task DeleteAsync(Guid id);
    Task<IEnumerable<ScheduleDayDto>> GetScheduleByEmployee(Guid employeeId, DateOnly startDate, DateOnly endDate);
    Task<IDictionary<EmployeeMinimalDto, IEnumerable<ScheduleDayDto>>> GetSchedule(DateOnly startDate, DateOnly endDate);
}
