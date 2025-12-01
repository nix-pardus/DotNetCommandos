using ServiceCenter.Domain.ValueObjects.Enums;

namespace ServiceCenter.Application.DTO.Responses;

/// <summary>
/// Сотрудник с назначенными заявками
/// </summary>
/// <param name="Id">Идентификатор сотрудника</param>
/// <param name="Name">Имя</param>
/// <param name="LastName">Фамилий</param>
/// <param name="Patronymic">Отчество</param>
/// <param name="Address">Домашний адрес</param>
/// <param name="Email">Электронная почта</param>
/// <param name="PhoneNumber">Номер телефона</param>
/// <param name="CreatedDate">Дата создания</param>
/// <param name="CreatedById">Идентификатор сотрудника, который создал этого сотрудника</param>
/// <param name="ModifiedDate">Дата последнего изменения</param>
/// <param name="ModifyById">Идентификатор сотрудника, который последний изменил этого сотрудника</param>
/// <param name="Role">Роль</param>
/// <param name="IsDeleted">Признак удаления</param>
/// <param name="AssignedOrders">Список назначенных заявок</param>
public record EmployeeWithOrdersResponse
(
    Guid Id,
    string Name,
    string LastName,
    string? Patronymic,
    string Address,
    string Email,
    string PhoneNumber,
    DateTime CreatedDate,
    Guid CreatedById,
    DateTime? ModifiedDate,
    Guid? ModifyById,
    RoleType Role,
    bool IsDeleted,
    IEnumerable<OrderAssignmentResponse> AssignedOrders
);
