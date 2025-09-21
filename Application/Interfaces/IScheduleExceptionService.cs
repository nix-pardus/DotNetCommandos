using ServiceCenter.Application.DTO.Schedule;

namespace ServiceCenter.Application.Interfaces;

/// <summary>
/// Сервис для работы с исключениями из графика
/// </summary>

public interface IScheduleExceptionService
{
    //TODO: пока так, но в итоге надо сделать отденьные dto
    Task CreateAsync(ScheduleExceptionDto dto);
    Task<ScheduleExceptionDto> UpdateAsync(ScheduleExceptionDto dto);
    /// <summary>
    /// Удаление исключения из графика по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор исключения из графика</param>
    Task DeleteAsync(Guid id);
    Task<IEnumerable<ScheduleExceptionDto>> GetAllByEmployeePaged(Guid employeeId, int page, int pageSize);
}
