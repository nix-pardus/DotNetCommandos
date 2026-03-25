using ServiceCenter.Domain.Entities;

namespace ServiceCenter.Domain.Interfaces;

/// <summary>
/// Репозиторий для работы с сущностью график работы
/// </summary>
public interface IScheduleRepository
{
    /// <summary>
    /// Получить все графики или графики сотрудника за период
    /// </summary>
    /// <param name="employeeId">id сотрудника</param>
    /// <param name="startDate">Дата начала запрашиваемого интервала</param>
    /// <param name="endDate">Дата окончания запрашиваемого интервала</param>
    Task<IEnumerable<Schedule>> GetAllByIntervalAsync(DateOnly startDate, DateOnly endDate, Guid? employeeId = null);

    /// <summary>
    /// Добавить график работы
    /// </summary>
    /// <param name="schedule">Сущность графика работы</param>
    Task AddAsync(Schedule schedule);

    /// <summary>
    /// Обновить существующий график
    /// </summary>
    /// <param name="schedule">Сущность графика</param>
    Task UpdateAsync(Schedule schedule);

    /// <summary>
    /// Удалить график по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор графика</param>
    Task DeleteAsync(Guid id);
}

