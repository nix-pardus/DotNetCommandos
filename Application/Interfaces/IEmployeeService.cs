using ServiceCenter.Application.DTO.Requests;
using ServiceCenter.Application.DTO.Responses;
using ServiceCenter.Application.DTO.Shared;

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
    Task CreateAsync(EmployeeCreateRequest dto);

    /// <summary>
    /// Обновление данных сотрудника
    /// </summary>
    /// <param name="dto">DTO сотрудника с обновлёнными данными</param>
    /// <returns>Задача выполнения операции</returns>
    Task UpdateAsync(EmployeeUpdateRequest dto);

    /// <summary>
    /// Удаление сотрудника по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор сотрудника</param>
    /// <returns>Задача выполнения операции</returns>
    Task DeleteAsync(Guid id);

    /// <summary>
    /// Получение сотрудников по фильтрам
    /// </summary>
    /// <param name="request">Параметры запроса</param>
    /// <returns>Постраничный список сотрудников</returns>
    Task<PagedResponse<EmployeeFullResponse>> GetByFiltersAsync(GetByFiltersRequest request);

    /// <summary>
    /// Получение сотрудников по фильтрам вместе с назначенными заказами
    /// </summary>
    /// <param name="request">Параметры запроса</param>
    /// <returns>Постраничный список сотрудников, включая назначенные заказы</returns>
    Task<PagedResponse<EmployeeWithOrdersResponse>> GetByFiltersWithOrdersAsync(GetByFiltersRequest request);
}
