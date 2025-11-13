namespace ServiceCenter.Application.DTO.Responses;

/// <summary>
/// Сотрудник
/// </summary>
/// <param name="Id">Идентификатор сотрудника</param>
/// <param name="Name">Имя</param>
/// <param name="LastName">Фамилия</param>
/// <param name="Patronymic">Отчество</param>
public record EmployeeMinimalResponse(
    Guid Id,
    string Name,
    string LastName,
    string? Patronymic
);

