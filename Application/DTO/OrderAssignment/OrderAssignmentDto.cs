using ServiceCenter.Domain.DTO.Order;

namespace ServiceCenter.Application.DTO.OrderAssignment;

public record OrderAssignmentDto
(
    Guid Id,
    OrderDto Order,
    bool IsPrimary,
    DateTime CreatedDate,
    DateTime? ModifiedDate,
    bool IsDeleted,
    Guid CreatedById,
    Guid? ModifiedById
);
