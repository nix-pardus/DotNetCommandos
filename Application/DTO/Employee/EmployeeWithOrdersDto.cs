using ServiceCenter.Application.DTO.OrderAssignment;
using ServiceCenter.Domain.DTO.Order;
using ServiceCenter.Domain.ValueObjects.Enums;

namespace ServiceCenter.Application.DTO.Employee;

public record EmployeeWithOrdersDto
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
    IEnumerable<OrderAssignmentDto> AssignedOrders
);
