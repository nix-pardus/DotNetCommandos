namespace ServiceCenter.Application.DTO.Assignment;

public record CreateAssignmentDto
(
    Guid OrderId,
    Guid EmployeeId,
    bool IsPrimary
);
