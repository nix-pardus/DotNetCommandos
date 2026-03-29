using System.Net.Http.Json;
using UI.Models;
using UI.Models.Assignments;

namespace UI.Services;

public class AssignmentService(IApiService apiClient) : IAssignmentService
{
    public async Task<Dictionary<Guid, List<OrderConflictInfo>>> CheckConflictsAsync(List<Guid> employeeIds, DateTime start, DateTime end, Guid? excludeOrderId = null)
    {
        var request = new
        {
            EmployeeIds = employeeIds,
            Start = start,
            End = end,
            ExcludeOrderId = excludeOrderId
        };

        return await apiClient.GetAsync<Dictionary<Guid, List<OrderConflictInfo>>>("api/Assignment/check-conflicts", request);
    }

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

    public async Task<List<OrderAssignmentResponse>> GetEmployeeAssignmentsAsync(Guid employeeId, DateTime start, DateTime end)
    {
        var url = $"api/Assignment/by-employee-poriod?employeeId={employeeId}&start={start:yyyy-MM-ddTHH:mm:ssZ}&end={end:yyyy-MM-ddTHH:mm:ssZ}";
        return await apiClient.GetFromJsonAsync<List<OrderAssignmentResponse>>(url);
    }

    public async Task<PagingResponse<OrderAssignmentResponse>> GetPagedAssignmetsByEmployeeAsync(Guid employeeId, DateTime? start, DateTime? end, OrderStatus? status, int page, int pageSize, string? sortBy, bool sortDesc)
    {
        var query = $"?employeeId={employeeId}&page={page}&pageSize={pageSize}";
        if (start.HasValue) query += $"&start={start.Value:yyyy-MM-ddTHH:mm:ssZ}";
        if (end.HasValue) query += $"&end={end.Value:yyyy-MM-ddTHH:mm:ssZ}";
        if (status.HasValue) query += $"&status={status.Value}";
        if (!string.IsNullOrEmpty(sortBy)) query += $"&sortBy={sortBy}";
        query += $"&sortDesc={sortDesc}";

        return await apiClient.GetFromJsonAsync<PagingResponse<OrderAssignmentResponse>>($"api/Assignment/paged-by-employee{query}");
    }
}
