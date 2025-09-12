using ServiceCenter.Application.DTO.Employee;
using ServiceCenter.Application.Interfaces;
using ServiceCenter.Application.Mappers;
using ServiceCenter.Domain.Interfaces;
using ServiceCenter.Domain.Queries;

namespace ServiceCenter.Application.Services;

/// <summary>
/// Сервис для работы с сотрудниками
/// Реализует <see cref="IEmployeeService"/>
/// </summary>
public class EmployeeService(IEmployeeRepository repository) : IEmployeeService
{
    /// <inheritdoc />
    public async Task CreateAsync(EmployeeDto dto)
    {
        await repository.AddAsync(EmployeeMapper.ToEntity(dto));
    }

    /// <inheritdoc />
    public async Task DeleteAsync(Guid id)
    {
        await repository.DeleteAsync(id);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<EmployeeDto>> GetAllAsync()
    {
        var employees = await repository.GetAllAsync();
        return employees.Select(EmployeeMapper.ToDto);
    }

    public async Task<(IEnumerable<EmployeeDto> Employees, int TotalCount)> GetEmployeesAsync(GetEmployeesQuery query)
    {
        var employees = await repository.GetEmployeesAsync(query);
        return (employees.Employees.Select(EmployeeMapper.ToDto), employees.TotalCount);
    }

    /// <inheritdoc />
    public async Task UpdateAsync(EmployeeDto dto)
    {
        await repository.UpdateAsync(EmployeeMapper.ToEntity(dto));
    }
}
