using ServiceCenter.Application.DTO.Employee;

namespace ServiceCenter.Application.Interfaces;

/// <summary>
/// Сервис для работы с сотрудниками
/// </summary>
public interface IEmployeeService
{
    /// <summary>
    /// Создание нового сотрудника
    /// </summary>
    /// <param name="dto">DTO сотрудника</param>
    /// <returns>Задача выполнения операции</returns>
    Task CreateAsync(EmployeeDto dto);

    /// <summary>
    /// Обновление данных сотрудника
    /// </summary>
    /// <param name="dto">DTO сотрудника с обновлёнными данными</param>
    /// <returns>Задача выполнения операции</returns>
    Task UpdateAsync(EmployeeDto dto);

    /// <summary>
    /// Удаление сотрудника по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор сотрудника</param>
    /// <returns>Задача выполнения операции</returns>
    Task DeleteAsync(Guid id);

    /// <summary>
    /// Получение списка всех сотрудников
    /// </summary>
    /// <returns>Список DTO сотрудников</returns>
    Task<IEnumerable<EmployeeDto>> GetAllAsync();
}
