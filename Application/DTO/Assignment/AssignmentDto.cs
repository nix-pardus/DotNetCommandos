namespace ServiceCenter.Application.DTO.Assignment;

public record AssignmentDto
(
    Guid Id,
    DateTime CreatedDate,
    DateTime? ModifiedDate,
    bool IsDeleted,
    Guid OrderId,
    Guid EmployeeId,
    bool IsPrimary,
    Guid CreatedById,
    Guid? ModifiedById
);
