using ServiceCenter.Application.DTO.Employee;
using ServiceCenter.Application.Interfaces;
using ServiceCenter.Application.Mappers;
using ServiceCenter.Domain.Interfaces;

namespace ServiceCenter.Application.Services;

/// <summary>
/// Сервис для работы с сотрудниками
/// Реализует <see cref="IEmployeeService"/>
/// </summary>
public class EmployeeService(IEmployeeRepository repository) : IEmployeeService
{
    /// <inheritdoc />
    public async Task CreateAsync(CreateEmployeeDto dto)
    {
        var employee = new EmployeeDto
        (
            Id: Guid.NewGuid(),
            Name: dto.Name,
            LastName: dto.LastName,
            Patronymic: dto.Patronymic,
            Address: dto.Address,
            Email: dto.Email,
            PhoneNumber: dto.PhoneNumber,
            CreatedDate: DateTime.UtcNow,
            CreatedById: Guid.Empty, // TODO: заменить на идентификатор текущего пользователя
            ModifiedDate: null,
            ModifyById: null,
            Role: dto.Role,
            IsDeleted: false
        );
        await repository.AddAsync(EmployeeMapper.ToEntity(employee));
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

    /// <inheritdoc />
    public async Task UpdateAsync(EmployeeDto dto)
    {
        await repository.UpdateAsync(EmployeeMapper.ToEntity(dto));
    }
}
