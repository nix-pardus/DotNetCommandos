using ServiceCenter.Domain.ValueObjects.Enums;

namespace ServiceCenter.Application.DTO.Employee;

/// <summary>
/// Сотрудник
/// </summary>
/// <param name="Id">Идентификатор сотрудника</param>
/// <param name="Name">Имя</param>
/// <param name="LastName">Фамилия</param>
/// <param name="Patronymic">Отчество</param>
public record EmployeeMinimalDto(
    Guid Id,
    string Name,
    string LastName,
    string? Patronymic
);

