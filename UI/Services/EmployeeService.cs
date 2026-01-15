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
        await _apiService.PostAsync<CreateEmployee>("api/Employee/create", employee);
    }
    public async Task<bool> UpdateEmployeeAsync(Employee employee)
    {
        return await _apiService.PutAsync($"api/Employee/update?id={employee.Id}", employee);
    }

    public async Task<bool> DeleteEmployeeAsync(Guid id)
    {
        return await _apiService.DeleteAsync($"api/Employee/delete?id={id}");
    }



}
