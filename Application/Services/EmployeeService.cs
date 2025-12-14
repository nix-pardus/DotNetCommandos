using Microsoft.EntityFrameworkCore;
using ServiceCenter.Application.DTO.Employee;
using ServiceCenter.Application.DTO.Shared;
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
    private readonly IPasswordHasher _passwordHasher;
    public EmployeeService(IEmployeeRepository repository, IPasswordHasher passwordHasher) : base(repository)
    {
        _passwordHasher = passwordHasher;
    }

    public async Task CreateAsync(CreateEmployeeDto dto)
    {
        var employee = new Employee()
        {

            Id = Guid.NewGuid(),
            Name = dto.Name,
            LastName = dto.LastName,
            Patronymic = dto.Patronymic,
            Address = dto.Address,
            Email = dto.Email,
            PasswordHash = _passwordHasher.HashPassword(dto.Password),
            PhoneNumber = dto.PhoneNumber,
            CreatedDate = DateTime.UtcNow,
            CreatedById = Guid.Empty, // TODO: заменить на идентификатор текущего пользователя, когда будет реализована аутентификация
            ModifiedDate = null,
            ModifiedById = null,
            Role = dto.Role,
            IsDeleted = false
        };
        await _repository.AddAsync(employee);
    }

    protected override EmployeeDto ToDto(Employee entity) => EmployeeMapper.ToDto(entity);

    protected override Employee ToEntity(EmployeeDto response) => EmployeeMapper.ToEntity(response);

    async Task<PagedResponse<EmployeeWithOrdersDto>> IEmployeeService.GetByFiltersWithOrdersAsync(GetByFiltersRequest request)
    {
        var filterConditions = request.Filters?.Select(f =>
            (f.Field, f.Operator.ToString(), f.Value)) ?? Enumerable.Empty<(string, string, string)>();

        var (items, totalCount) = await _repository.GetByFiltersPagedWithIncludesAsync(
            filterConditions,
            request.LogicalOperator,
            request.PageNumber,
            request.PageSize,
            q => q.Include(e => e.AssignedOrders.Where(x => x.IsDeleted == false)).ThenInclude(ao => ao.Order)
        );

        return new PagedResponse<EmployeeWithOrdersDto>
        {
            Items = items.Select(EmployeeMapper.ToWithOrdersDto).ToList(),
            TotalCount = totalCount,
            PageNumber = request.PageNumber,
            PageSize = request.PageSize
        };
    }

}
