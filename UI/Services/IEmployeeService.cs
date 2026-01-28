using UI.Models;
using UI.Models.Employees;

namespace UI.Services;

public interface IEmployeeService
{
    Task<PagingResponse<Employee>> GetAllEmployeesAsync(GetByFiltersRequest request);
    Task<Employee> GetEmployeeByIdAsync(Guid id);
    Task CreateEmployeeAsync(CreateEmployee employee);
    Task<bool> UpdateEmployeeAsync(EmployeeUpdate employee);
    Task<bool> DeleteEmployeeAsync(Guid id);
}
