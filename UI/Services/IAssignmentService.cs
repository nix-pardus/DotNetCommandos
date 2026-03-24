using UI.Models;
using UI.Models.Assignments;

namespace UI.Services;

public interface IAssignmentService
{
    Task<PagingResponse<Assignment>> GetAssignmentsAsync(GetByFiltersRequest request);
    Task<PagingResponse<Assignment>> GetAssignmentsByOrderIdAsync(Guid orderId);
    Task CreateAssignmentAsync(AssignmentCreate assignment);
    Task DeleteAssignmentAsync(Guid id);
    Task<Dictionary<Guid, List<OrderConflictInfo>>> CheckConflictsAsync(List<Guid> employeeIds, DateTime start, DateTime end, Guid? excludeOrderId = null);
    Task<List<OrderAssignmentResponse>> GetEmployeeAssignmentsAsync(Guid employeeId, DateTime start, DateTime end);
}
