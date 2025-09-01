using UI.Models;

namespace UI.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IApiService _apiService;

    public EmployeeService(IApiService apiService)
    {
        _apiService = apiService;
    }
    public async Task<List<Employee>> GetAllEmployeesAsync()
    {
        return await _apiService.GetAsync<List<Employee>>("api/Employee/getAll");
    }
    public Task<Employee> GetEmployeeByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<Employee> CreateEmployeeAsync(Employee employee)
    {
        return await _apiService.PostAsync<Employee>("api/create", employee);
    }
    public async Task<bool> UpdateEmployeeAsync(Employee employee)
    {
        return await _apiService.PutAsync($"api/update/{employee.Id}", employee);
    }

    public async Task<bool> DeleteEmployeeAsync(Guid id)
    {
        return await _apiService.DeleteAsync($"api/delete/{id}");
    }



}
