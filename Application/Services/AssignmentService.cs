using ServiceCenter.Application.DTO.Requests;
using ServiceCenter.Application.DTO.Responses;
using ServiceCenter.Application.DTO.Shared;
using ServiceCenter.Application.Interfaces;
using ServiceCenter.Application.Mappers;
using ServiceCenter.Domain.Entities;
using ServiceCenter.Domain.Interfaces;

namespace ServiceCenter.Application.Services;

public class AssignmentService : BaseService<OrderEmployee, AssignmentCreateRequest, AssignmentUpdateRequest, AssignmentResponse, IAssignmentRepository>, IAssignmentService
{
    public AssignmentService(IAssignmentRepository repository, ICurrentUserService currentUserService) : base(repository, currentUserService)
    {
    }

    public async Task CreateAsync(AssignmentCreateRequest request)
    {
        var filterConditions = new List<(string, string, string)>
        {
            ("OrderId", "Equals", $"{request.OrderId}"),
            ("EmployeeId", "Equals", $"{request.EmployeeId}"),
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
            var newAssignment = new AssignmentResponse
            (
                Id: Guid.NewGuid(),
                CreatedDate: DateTime.UtcNow,
                ModifiedDate: null,
                IsDeleted: false,
                OrderId: request.OrderId,
                EmployeeId: request.EmployeeId,
                IsPrimary: request.IsPrimary,
                CreatedById: _currentUserService.UserId ?? Guid.Empty,
                ModifiedById: null
            );
            await _repository.AddAsync(AssignmentMapper.ToEntity(newAssignment));
        }
    }

    public Task<PagedResponse<AssignmentResponse>> GetAllByEmployeeIdAsync(Guid employeeId)
    {
        throw new NotImplementedException();
    }

    public Task<PagedResponse<AssignmentResponse>> GetAllByOrderIdAsync(Guid orderId)
    {
        throw new NotImplementedException();
    }

    protected override AssignmentResponse ToDto(OrderEmployee entity) => AssignmentMapper.ToResponse(entity);

    protected override OrderEmployee ToEntity(AssignmentCreateRequest request) => AssignmentMapper.ToEntity(request);

    protected override OrderEmployee ToEntity(AssignmentUpdateRequest request) => AssignmentMapper.ToEntity(request);

    //Task IAssignmentService.UpdateAsync(AssignmentUpdateRequest dto)
    //{
    //    return UpdateAsync(dto);
    //}
}
