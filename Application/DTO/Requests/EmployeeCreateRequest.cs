using ServiceCenter.Domain.ValueObjects.Enums;

namespace ServiceCenter.Application.DTO.Requests;

/// <summary>
/// Сотрудник
/// </summary>
/// <param name="Name">Имя</param>
/// <param name="LastName">Фамилия</param>
/// <param name="Patronymic">Отчество</param>
/// <param name="Address">Домашний адрес</param>
/// <param name="Email">Электронная почта</param>
/// <param name="PhoneNumber">Номер телефона</param>
/// <param name="Role">Роль</param>
public record EmployeeCreateRequest
(
    string Name,
    string LastName,
    string? Patronymic,
    string Address,
    string Email,
    string PhoneNumber,
    RoleType Role
);
