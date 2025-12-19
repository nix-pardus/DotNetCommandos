using UI.Models;

namespace UI.Services;

public interface IEmployeeService
{
    Task<PagingResponse<Employee>> GetAllEmployeesAsync(GetByFiltersRequest request);
    Task<Employee> GetEmployeeByIdAsync(Guid id);
    Task<Employee> CreateEmployeeAsync(Employee employee);
    Task<bool> UpdateEmployeeAsync(Employee employee);
    Task<bool> DeleteEmployeeAsync(Guid id);
}
