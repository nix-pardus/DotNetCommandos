using Microsoft.EntityFrameworkCore;
using ServiceCenter.Application.DTO.Requests;
using ServiceCenter.Application.DTO.Responses;
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
public class EmployeeService : BaseService<Employee, EmployeeCreateRequest, EmployeeUpdateRequest, EmployeeFullResponse, IEmployeeRepository>, IEmployeeService
{
    private readonly IPasswordHasher _passwordHasher;
    public EmployeeService(IEmployeeRepository repository, IPasswordHasher passwordHasher, ICurrentUserService currentUserService) : base(repository, currentUserService)
    {
        _passwordHasher = passwordHasher;
    }

    protected override EmployeeFullResponse ToDto(Employee entity) => EmployeeMapper.ToResponse(entity);
    protected override Employee ToEntity(EmployeeCreateRequest dto) => EmployeeMapper.ToEntity(dto);
    protected override Employee ToEntity(EmployeeUpdateRequest dto) => EmployeeMapper.ToEntity(dto);

    public async Task<PagedResponse<EmployeeWithOrdersResponse>> GetByFiltersWithOrdersAsync(GetByFiltersRequest request)
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

        return new PagedResponse<EmployeeWithOrdersResponse>
        {
            Items = items.Select(EmployeeMapper.ToWithOrdersDto).ToList(),
            TotalCount = totalCount,
            PageNumber = request.PageNumber,
            PageSize = request.PageSize
        };
    }

}
