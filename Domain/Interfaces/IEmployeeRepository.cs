using ServiceCenter.Domain.Entities;
using ServiceCenter.Domain.Queries;
using ServiceCenter.Domain.ValueObjects.Enums;

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
    /// Получить список сотрудников с фильтрацией и пагинацией
    /// </summary>
    /// <param name="query">Обект с параметрами запроса</param>
    /// <returns>Кортеж, содержащий список сотрудников и общее количество найденых сотрудников</returns>
    Task<(IEnumerable<Employee> Employees, int TotalCount)> GetEmployeesAsync(GetEmployeesQuery query);

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
