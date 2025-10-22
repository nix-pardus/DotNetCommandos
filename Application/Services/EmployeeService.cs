using ServiceCenter.Application.DTO.Employee;
using ServiceCenter.Application.Interfaces;
using ServiceCenter.Application.Mappers;
using ServiceCenter.Domain.Entities;
using ServiceCenter.Domain.Interfaces;

namespace ServiceCenter.Application.Services;

/// <summary>
/// Сервис для работы с сотрудниками
/// Реализует <see cref="IEmployeeService"/>
/// </summary>
public class EmployeeService : BaseService<Employee, EmployeeDto, IEmployeeRepository>, IEmployeeService
{
    public EmployeeService(IEmployeeRepository repository) : base(repository)
    {
    }

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
        CreatedById: Guid.Empty, // TODO: заменить на идентификатор текущего пользователя, когда будет реализована аутентификация
        ModifiedDate: null,
        ModifyById: null,
        Role: dto.Role,
        IsDeleted: false
    );
        await _repository.AddAsync(EmployeeMapper.ToEntity(employee));
    }

    protected override EmployeeDto ToDto(Employee entity) => EmployeeMapper.ToDto(entity);

    protected override Employee ToEntity(EmployeeDto response) => EmployeeMapper.ToEntity(response);
}
