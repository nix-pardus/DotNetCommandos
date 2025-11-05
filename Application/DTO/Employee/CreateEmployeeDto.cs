using ServiceCenter.Domain.ValueObjects.Enums;

namespace ServiceCenter.Application.DTO.Employee;

public record CreateEmployeeDto
(
    string Name,
    string LastName,
    string? Patronymic,
    string Address,
    string Email,
    string PhoneNumber,
    RoleType Role
);
