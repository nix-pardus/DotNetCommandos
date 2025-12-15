namespace ServiceCenter.Application.DTO.Requests;

/// <summary>
/// Для создания назначения
/// </summary>
/// <param name="OrderId">Идентификатор заявки</param>
/// <param name="EmployeeId">Идентификатор сотрудника</param>
/// <param name="IsPrimary">Является ли сотрудник главным на заявке</param>
public record AssignmentCreateRequest
(
    Guid OrderId,
    Guid EmployeeId,
    bool IsPrimary
);
