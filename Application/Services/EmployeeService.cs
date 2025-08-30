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

    public async Task DeleteAsync(Guid id)
    {
        await _repository.DeleteAsync(id);
    }

    public async Task<IEnumerable<EmployeeDto>> GetAllAsync()
    {
        var employees = await _repository.GetAllAsync();
        return employees.Select(employee => new EmployeeDto
        {
            Address = employee.Address,
            CreatedDate = employee.CreatedDate,
            Creator = employee.Creator,
            Email = employee.Email,
            Id = employee.Id,
            IsDeleted = employee.IsDeleted,
            LastName = employee.LastName,
            Name = employee.Name,
            Patronymic = employee.Patronymic,
            PhoneNumber = employee.PhoneNumber,
            Role = employee.Role,
        });
    }

    public async Task UpdateAsync(EmployeeDto dto)
    {
        await _repository.UpdateAsync(new Employee
        {
            Address = dto.Address,
            CreatedDate = dto.CreatedDate,
            Creator = dto.Creator,
            Email = dto.Email,
            Id = dto.Id,
            IsDeleted = dto.IsDeleted,
            LastName = dto.LastName,
            Name = dto.Name,
            Patronymic = dto.Patronymic,
            PhoneNumber = dto.PhoneNumber,
            Role = dto.Role,
        });
    }
}
