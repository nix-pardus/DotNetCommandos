using UI.Models;
using UI.Models.Employees;

namespace UI.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IApiService _apiService;

    public EmployeeService(IApiService apiService)
    {
        _apiService = apiService;
    }
    public async Task<PagingResponse<Employee>> GetAllEmployeesAsync(GetByFiltersRequest request)
    {
        return await _apiService.GetAsync<PagingResponse<Employee>>("api/Employee/get-by-filters", request);
    }
    public Task<Employee> GetEmployeeByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task CreateEmployeeAsync(CreateEmployee employee)
    {
        await _apiService.PostAsync("api/Employee/create", employee);
    }
    public async Task<bool> UpdateEmployeeAsync(EmployeeUpdate employee)
    {
        return await _apiService.PutAsync($"api/Employee/update?id={employee.Id}", employee);
    }

    public async Task<bool> DeleteEmployeeAsync(Guid id)
    {
        return await _apiService.DeleteAsync($"api/Employee/delete?id={id}");
    }

    public async Task<List<EmployeeMinimal>> GetEmployeeMinimalByIdsAsync(IEnumerable<Guid> employeeIds)
    {
        try
        {
            if (!employeeIds.Any())
                return new List<EmployeeMinimal>();

            var request = new GetByFiltersRequest
            {
                PageNumber = 1,
                PageSize = 1000,
                Filters = new List<GetByFiltersRequest.FilterCondition>
                {
                    new()
                    {
                        Field = "Id",
                        Operator = GetByFiltersRequest.FilterOperator.In,
                        Value = string.Join(",", employeeIds.Distinct()),
                        ValueType = "string"
                    }
                },
                LogicalOperator = "AND",
                SortBy = new List<GetByFiltersRequest.Sorting>
                {
                    new()
                    {
                        Field = "LastName",
                        Direction = false
                    }
                }
            };

            var response = await GetAllEmployeesAsync(request);
            return response.Items
                .Select(EmployeeMinimal.FromEmployee)
                .ToList();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка получения минимальных данных сотрудников: {ex.Message}");
            return new List<EmployeeMinimal>();
        }
    }
}
