using ServiceCenter.Application.DTO.Requests;
using ServiceCenter.Application.DTO.Responses;
using ServiceCenter.Application.DTO.Shared;
using ServiceCenter.Domain.ValueObjects.Enums;

namespace ServiceCenter.Application.Interfaces;

public interface IAssignmentService
{
    /// <summary>
    /// Создание нового назначения
    /// </summary>
    /// <param name="dto">DTO создания/изменения назначения</param>
    /// <returns>Task</returns>
    Task CreateAsync(AssignmentCreateRequest dto);

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
    Task UpdateAsync(AssignmentUpdateRequest dto);

    /// <summary>
    /// Получение всех назначений по заявке
    /// </summary>
    /// <param name="orderId">Id заявки</param>
    /// <returns>Постраничный список назначений</returns>
    Task<PagedResponse<AssignmentResponse>> GetAllByOrderIdAsync(Guid orderId);

    /// <summary>
    /// Получение всех назначений сотрудника
    /// </summary>
    /// <param name="employeeId">Id сотрудника</param>
    /// <returns>Постраничный список назначений</returns>
    Task<PagedResponse<AssignmentResponse>> GetAllByEmployeeIdAsync(Guid employeeId);

    /// <summary>
    /// Получение назначений по фильтрам
    /// </summary>
    /// <param name="request">Параметры запроса</param>
    /// <returns>Постраничный список назначений</returns>
    Task<PagedResponse<AssignmentResponse>> GetByFiltersAsync(GetByFiltersRequest request);

    /// <summary>
    /// Проверяет пересечение с другими заявками
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<Dictionary<Guid, List<OrderConflictInfo>>> CheckConflictsAsync(AssignmentConflictCheckRequest request);

    Task<List<OrderAssignmentResponse>> GetAssignmentsByEmployeeAndPeriodAsync(Guid employeeId, DateTime start, DateTime end);

    Task<PagedResponse<OrderAssignmentResponse>> GetPagedAssignmentsByEmployeeAsync(
        Guid employeeId,
        DateTime? start,
        DateTime? end,
        OrderStatus? status,
        int page,
        int pageSize,
        string? sortBy,
        bool sortDesc
    );
}
