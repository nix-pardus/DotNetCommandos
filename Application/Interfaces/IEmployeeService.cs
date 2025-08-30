using Domain.DTO.Employee;

namespace Application.Interfaces;

public interface IEmployeeService
{
    Task CreateAsync(EmployeeDto dto);
    Task UpdateAsync(EmployeeDto dto);
    Task DeleteAsync(Guid id);
    Task<IEnumerable<EmployeeDto>> GetAllAsync();
}
