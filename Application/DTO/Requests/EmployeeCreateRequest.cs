using ServiceCenter.Domain.ValueObjects.Enums;

namespace ServiceCenter.Application.DTO.Requests;

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
