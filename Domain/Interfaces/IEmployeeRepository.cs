using ServiceCenter.Domain.Entities;

namespace ServiceCenter.Domain.Interfaces;

/// <summary>
/// Репозиторий для работы с сущностью Сотрудник
/// </summary>
public interface IEmployeeRepository
{
    /// <summary>
    /// Получить всех сотрудников
    /// </summary>
    Task<IEnumerable<Employee>> GetAllAsync();

    /// <summary>
    /// Получить всех активных сотрудников
    /// </summary>
    Task<IEnumerable<Employee>> GetAllActiveAsync();

    /// <summary>
    /// Получить сотрудника по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор сотрудника</param>
    Task<Employee> GetByIdAsync(Guid id);

    /// <summary>
    /// Добавить нового сотрудника
    /// </summary>
    /// <param name="employee">Сущность сотрудника</param>
    Task AddAsync(Employee employee);

    /// <summary>
    /// Обновить существующего сотрудника
    /// </summary>
    /// <param name="employee">Сущность сотрудника</param>
    Task UpdateAsync(Employee employee);

    /// <summary>
    /// Удалить сотрудника по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор сотрудника</param>
    Task DeleteAsync(Guid id);
}
