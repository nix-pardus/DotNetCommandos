using ServiceCenter.Domain.Entities;

namespace ServiceCenter.Domain.Interfaces;

/// <summary>
/// Репозиторий для работы с сущностью исключения из графика
/// </summary>
public interface IScheduleExceptionRepository
{
    /// <summary>
    /// Получить все исключения сотрудника за период
    /// </summary>
    Task<IEnumerable<ScheduleException>> GetByEmployeePaged(Guid employeeId, int page, int pageSize);
    Task<IEnumerable<ScheduleException>> GetAllByIntervalAsync(Guid? employeeId, DateOnly startDate, DateOnly endDate);
    Task<IEnumerable<ScheduleException>> GetAllByIntervalAsync(DateOnly startDate, DateOnly endDate);
    /// <summary>
    /// Добавить исключение из графика
    /// </summary>
    /// <param name="scheduleException">Сущность исключения из графика</param>
    Task AddAsync(ScheduleException scheduleException);

    /// <summary>
    /// Обновить существующее исключение из графика
    /// </summary>
    /// <param name="scheduleException">Сущность исключения из графика</param>
    Task UpdateAsync(ScheduleException scheduleException);

    /// <summary>
    /// Удалить исключение из графика по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор исключения из графика</param>
    Task DeleteAsync(Guid id);
}

