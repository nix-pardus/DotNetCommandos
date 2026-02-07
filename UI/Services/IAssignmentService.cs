using UI.Models;
using UI.Models.Assignments;

namespace UI.Services;

public interface IAssignmentService
{
    Task<PagingResponse<Assignment>> GetAssignmentsAsync(GetByFiltersRequest request);
    Task<PagingResponse<Assignment>> GetAssignmentsByOrderIdAsync(Guid orderId);
    Task CreateAssignmentAsync(AssignmentCreate assignment);
    Task DeleteAssignmentAsync(Guid id);
}
