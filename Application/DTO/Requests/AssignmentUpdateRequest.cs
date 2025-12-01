namespace ServiceCenter.Application.DTO.Requests;

/// <summary>
/// Для обновления назначения
/// </summary>
/// <param name="OrderId">Идентификатор заявки</param>
/// <param name="EmployeeId">Идентификатор сотрудника</param>
/// <param name="IsPrimary">Является ли сотрудник главным на заявке</param>
/// <param name="IsDelete">Признак удаления</param>
public record AssignmentUpdateRequest
(
    Guid OrderId,
    Guid EmployeeId,
    bool IsPrimary,
    bool IsDelete
);
