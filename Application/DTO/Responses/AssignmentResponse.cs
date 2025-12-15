namespace ServiceCenter.Application.DTO.Responses;

/// <summary>
/// Назначение на заявку
/// </summary>
/// <param name="Id">Идентификатор назначения</param>
/// <param name="CreatedDate">Дата назначения</param>
/// <param name="ModifiedDate">Дата изменения назначения</param>
/// <param name="IsDeleted">Признак удаления</param>
/// <param name="OrderId">Идентификатор заявки</param>
/// <param name="EmployeeId">Идентификатор сотрудника</param>
/// <param name="IsPrimary">Является ли сотрудник главным на заявке</param>
/// <param name="CreatedById">Идентификатор сотрудника, который назначил</param>
/// <param name="ModifiedById">Идентификатор сотрудника, который изменил назначение</param>
public record AssignmentResponse
(
    Guid Id,
    DateTime CreatedDate,
    DateTime? ModifiedDate,
    bool IsDeleted,
    Guid OrderId,
    Guid EmployeeId,
    bool IsPrimary,
    Guid CreatedById,
    Guid? ModifiedById
);
