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

    public async Task<Dictionary<Guid, List<OrderConflictInfo>>> CheckConflictsAsync(AssignmentConflictCheckRequest request)
    {
        var result = new Dictionary<Guid, List<OrderConflictInfo>>();
        foreach(var employeeId in request.EmployeeIds)
        {
            var conflicts = await _repository.GetConflictingAssignmentsAsync(employeeId, request.Start, request.End, request.ExcludeOrderId);
            if(conflicts.Any())
            {
                result[employeeId] = conflicts.Select(oe => new OrderConflictInfo
                {
                    OrderId = oe.OrderId,
                    StartDateTime = oe.Order.StartDateTime.Value,
                    EndDateTime = oe.Order.EndDateTime.Value,
                    ClientName = oe.Order.Client != null
                        ? $"{oe.Order.Client.LastName} {oe.Order.Client.Name}"
                        : "Неизвестно"
                }).ToList();
            }
        }
        return result;
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
            var newAssignment = AssignmentMapper.ToEntity(request);
            
            newAssignment.Id = Guid.NewGuid();
            newAssignment.CreatedDate = DateTime.UtcNow;
            newAssignment.IsDeleted = false;
            newAssignment.CreatedById = _currentUserService.UserId ?? Guid.Empty;

            await _repository.AddAsync(newAssignment);
        }
    }

    public Task<PagedResponse<AssignmentResponse>> GetAllByEmployeeIdAsync(Guid employeeId)
    {
        throw new NotImplementedException();
    }

    public async Task<PagedResponse<AssignmentResponse>> GetAllByOrderIdAsync(Guid orderId)
    {
        var filterConditions = new List<(string, string, string)>
        {
            ("OrderId", "Equals", $"{orderId}"),
            ("IsDeleted", "Equals", "false")
        };

        var assignments = await _repository.GetByFiltersPagedAsync(filterConditions, "AND", 1, 100);

        return new PagedResponse<AssignmentResponse>
        {
            TotalCount = assignments.TotalCount,
            PageNumber = 1,
            PageSize = 100,
            Items = assignments.Items.Select(a => new AssignmentResponse
            (
                Id: a.Id,
                CreatedDate: a.CreatedDate,
                ModifiedDate: a.ModifiedDate,
                IsDeleted: a.IsDeleted,
                OrderId: a.OrderId,
                EmployeeId: a.EmployeeId,
                IsPrimary: a.IsPrimary,
                CreatedById: a.CreatedById,
                ModifiedById: a.ModifiedById
            )).ToList()
        };
    }

    public async Task<List<OrderAssignmentResponse>> GetAssignmentsByEmployeeAndPeriodAsync(Guid employeeId, DateTime start, DateTime end)
    {
        var assignments = await _repository.GetAssignmentsByEmployeeAndPeriodAsync(employeeId, start, end);

        return assignments.Select(a => new OrderAssignmentResponse
        (
            a.Id,
            OrderMapper.ToResponseWithClient(a.Order),
            a.IsPrimary,
            a.CreatedDate,
            a.ModifiedDate,
            a.IsDeleted,
            a.CreatedById,
            a.ModifiedById
        )).ToList();
    }

    protected override AssignmentResponse ToDto(OrderEmployee entity) => AssignmentMapper.ToResponse(entity);

    protected override OrderEmployee ToEntity(AssignmentCreateRequest request) => AssignmentMapper.ToEntity(request);

    protected override OrderEmployee ToEntity(AssignmentUpdateRequest request) => AssignmentMapper.ToEntity(request);

    //Task IAssignmentService.UpdateAsync(AssignmentUpdateRequest dto)
    //{
    //    return UpdateAsync(dto);
    //}
}
