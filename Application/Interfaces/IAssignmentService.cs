using ServiceCenter.Application.DTO.Assignment;
using ServiceCenter.Application.DTO.Shared;

namespace ServiceCenter.Application.Interfaces;

public interface IAssignmentService
{
    /// <summary>
    /// Создание нового назначения
    /// </summary>
    /// <param name="dto">DTO создания/изменения назначения</param>
    /// <returns>Task</returns>
    Task CreateAsync(CreateAssignmentDto dto);

    /// <summary>
    /// Удаление назначения
    /// </summary>
    /// <param name="orderEmployeeId">Id назначения</param>
    /// <returns>Task</returns>
    Task DeleteAsync(Guid orderEmployeeId);

    /// <summary>
    /// Изменение назначения
    /// </summary>
    /// <param name="dto">DTO создания/изменения назначения</param>
    /// <returns>Task</returns>
    Task UpdateAsync(CreateAssignmentDto dto);

    /// <summary>
    /// Получение всех назначений по заявке
    /// </summary>
    /// <param name="orderId">Id заявки</param>
    /// <returns>Постраничный список назначений</returns>
    Task<PagedResponse<AssignmentDto>> GetAllByOrderIdAsync(Guid orderId);

    /// <summary>
    /// Получение всех назначений сотрудника
    /// </summary>
    /// <param name="employeeId">Id сотрудника</param>
    /// <returns>Постраничный список назначений</returns>
    Task<PagedResponse<AssignmentDto>> GetAllByEmployeeIdAsync(Guid employeeId);

    /// <summary>
    /// Получение назначений по фильтрам
    /// </summary>
    /// <param name="request">Параметры запроса</param>
    /// <returns>Постраничный список назначений</returns>
    Task<PagedResponse<AssignmentDto>> GetByFiltersAsync(GetByFiltersRequest request);
}
