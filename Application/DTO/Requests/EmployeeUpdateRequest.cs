using ServiceCenter.Domain.ValueObjects.Enums;

namespace ServiceCenter.Application.DTO.Requests;

public record EmployeeUpdateRequest
(
    Guid Id,
    string Name,
    string LastName,
    string Patronymic,
    string Address,
    string Email,
    string PhoneNumber,
    RoleType Role,
    bool IsDeleted
);
