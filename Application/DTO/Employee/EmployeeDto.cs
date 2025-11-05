using ServiceCenter.Domain.ValueObjects.Enums;

namespace ServiceCenter.Application.DTO.Employee;

/// <summary>
/// Сотрудник
/// </summary>
/// <param name="Id">Идентификатор сотрудника</param>
/// <param name="Name">Имя</param>
/// <param name="LastName">Фамилия</param>
/// <param name="Patronymic">Отчество</param>
/// <param name="Address">Домашний адрес</param>
/// <param name="Email">Электронная почта</param>
/// <param name="PhoneNumber">Телефон</param>
/// <param name="CreatedDate">Дата создания записи</param>
/// <param name="CreatedById">Идентификатор создавшего сотрудника</param>
/// <param name="ModifiedDate">Дата изменения записи</param>
/// <param name="ModifiedById">Идентификатор изменившего сотрудника</param>
/// <param name="Role">Роль в системе</param>
/// <param name="IsDeleted">Признак удаления/неактуальности</param>
public record EmployeeDto(
    Guid Id,
    DateTime CreatedDate,
    DateTime? ModifiedDate,
    bool IsDeleted,
    string Name,
    string LastName,
    string? Patronymic,
    string Address,
    string Email,
    string PhoneNumber,
    Guid CreatedById,
    Guid? ModifiedById,
    RoleType Role
);

