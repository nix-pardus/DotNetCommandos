using ServiceCenter.Application.DTO.Responses;

namespace ServiceCenter.Application.DTO.OrderAssignment;

public record OrderAssignmentDto
(
    Guid Id,
    OrderFullResponse Order,
    bool IsPrimary,
    DateTime CreatedDate,
    DateTime? ModifiedDate,
    bool IsDeleted,
    Guid CreatedById,
    Guid? ModifiedById
);
