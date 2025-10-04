using ServiceCenter.Application.DTO.Requests;
using ServiceCenter.Application.DTO.Responses;
using ServiceCenter.Application.DTO.Shared;

namespace ServiceCenter.Application.Interfaces;

/// <summary>
/// Сервис для работы с графиком работы
/// </summary>

public interface IScheduleService
{
    //TODO: пока так, но в итоге надо сделать отденьные dto
    Task CreateAsync(ScheduleCreateRequest dto);
    Task<ScheduleFullResponse> UpdateAsync(ScheduleUpdateRequest dto);
    /// <summary>
    /// Удаление графика работы по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор графика</param>
    Task DeleteAsync(Guid id);
    Task<IEnumerable<ScheduleDayDto>> GetScheduleByEmployee(Guid employeeId, DateOnly startDate, DateOnly endDate);
    Task<IDictionary<EmployeeMinimalResponse, IEnumerable<ScheduleDayDto>>> GetSchedule(DateOnly startDate, DateOnly endDate);
}
