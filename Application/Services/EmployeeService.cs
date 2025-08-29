using Application.Interfaces;
using Domain.Aggregates;
using Domain.DTO.Employee;
using Domain.Interfaces;

namespace Application.Services;

public class EmployeeService : IEmployeeService
{
    public readonly IEmployeeRepository _repository;

    public EmployeeService(IEmployeeRepository repository)
    {
        _repository = repository;
    }

    public async Task CreateAsync(EmployeeDto dto)
    {
        var employee = new Employee(dto);
        await _repository.AddAsync(employee);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        return await _repository.DeleteAsync(id);
    }

    public Task<EmployeeDto> UpdateAsync(EmployeeDto dto)
    {
        throw new NotImplementedException();
    }
}
