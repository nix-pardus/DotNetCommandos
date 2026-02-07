using System.Net.Http.Json;
using UI.Models;
using UI.Models.Assignments;

namespace UI.Services;

public class AssignmentService(IApiService apiClient) : IAssignmentService
{
    public async Task CreateAssignmentAsync(AssignmentCreate assignment)
    {
        await apiClient.PostAsync("api/Assignment/create", assignment);
    }

    public async Task DeleteAssignmentAsync(Guid id)
    {
        await apiClient.DeleteAsync($"api/Assignment/delete?id={id}");
    }

    public async Task<PagingResponse<Assignment>> GetAssignmentsAsync(GetByFiltersRequest request)
    {
        return await apiClient.GetAsync<PagingResponse<Assignment>>("api/Assignment/get-by-filters", request);
    }

    public async Task<PagingResponse<Assignment>> GetAssignmentsByOrderIdAsync(Guid orderId)
    {
        return await apiClient.GetAsync<PagingResponse<Assignment>>("api/Assignment/get-by-order-id", orderId);
    }
}
