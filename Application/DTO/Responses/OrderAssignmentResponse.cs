namespace ServiceCenter.Application.DTO.Responses;

/// <summary>
/// Назначенная заявка на сотрудника
/// </summary>
/// <param name="Id">Идентификатор назначения</param>
/// <param name="Order">Заявка</param>
/// <param name="IsPrimary">Является ли сотрудник главным на заявке</param>
/// <param name="CreatedDate">Дата назначения</param>
/// <param name="ModifiedDate">Дата изменения назначения</param>
/// <param name="IsDeleted">Признак удаления</param>
/// <param name="CreatedById">Идентификатор сотрудника, который назначил</param>
/// <param name="ModifiedById">Идентификатор сотрудника, который изменил назначение</param>
public record OrderAssignmentResponse //используется в EmployeeWithOrdersResponse для отображения списка назначений
(
    Guid Id,
    OrderFullResponse Order,
    bool IsPrimary,
    DateTime CreatedDate,
    DateTime? ModifiedDate,
    bool IsDeleted,
    Guid CreatedById,
    Guid? ModifiedById
);
