using ServiceCenter.Domain.DTO.Order;

namespace ServiceCenter.Application.DTO.OrderAssignment;

public record OrderAssignmentDto
(
    OrderDto Order,
    bool IsPrimary,
    DateTime CreatedDate,
    DateTime? ModifiedDate,
    bool IsDeleted
);
