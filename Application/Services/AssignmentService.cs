using ServiceCenter.Application.DTO.Assignment;
using ServiceCenter.Application.DTO.Shared;
using ServiceCenter.Application.Interfaces;
using ServiceCenter.Application.Mappers;
using ServiceCenter.Domain.Entities;
using ServiceCenter.Domain.Interfaces;

namespace ServiceCenter.Application.Services;

public class AssignmentService : BaseService<OrderEmployee, AssignmentDto, IAssignmentRepository>, IAssignmentService
{
    public AssignmentService(IAssignmentRepository repository) : base(repository)
    {
    }

    public async Task CreateAsync(CreateAssignmentDto dto)
    {
        var filterConditions = new List<(string, string, string)>
        {
            ("OrderId", "Equals", $"{dto.OrderId}"),
            ("EmployeeId", "Equals", $"{dto.EmployeeId}"),
            ("IsDeleted", "Equals", "false")
        };
        var assignment = await _repository.GetByFiltersPagedAsync
        (
            filterConditions,
            "",
            1,
            10
        );
        if (assignment.TotalCount == 0)
        {
            var newAssignment = new AssignmentDto
            (
                Id: Guid.NewGuid(),
                CreatedDate: DateTime.UtcNow,
                ModifiedDate: null,
                IsDeleted: false,
                OrderId: dto.OrderId,
                EmployeeId: dto.EmployeeId,
                IsPrimary: dto.IsPrimary,
                CreatedById: Guid.Empty, // TODO: заменить на идентификатор текущего пользователя, когда будет реализована аутентификация
                ModifiedById: null
            );
            await _repository.AddAsync(AssignmentMapper.ToEntity(newAssignment));
        }
    }

    public Task<PagedResponse<AssignmentDto>> GetAllByEmployeeIdAsync(Guid employeeId)
    {
        throw new NotImplementedException();
    }

    public Task<PagedResponse<AssignmentDto>> GetAllByOrderIdAsync(Guid orderId)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(CreateAssignmentDto dto)
    {
        throw new NotImplementedException();
    }

    protected override AssignmentDto ToDto(OrderEmployee entity) => AssignmentMapper.ToDto(entity);

    protected override OrderEmployee ToEntity(AssignmentDto response) => AssignmentMapper.ToEntity(response);
}
