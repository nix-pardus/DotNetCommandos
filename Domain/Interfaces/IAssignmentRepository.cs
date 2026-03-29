using ServiceCenter.Domain.Entities;
using ServiceCenter.Domain.ValueObjects.Enums;

namespace ServiceCenter.Domain.Interfaces;

public interface IAssignmentRepository : IRepository<OrderEmployee>
{
    Task<List<OrderEmployee>> GetConflictingAssignmentsAsync(Guid employeeId, DateTime start, DateTime end, Guid? excludeOrderId = null);
    Task<List<OrderEmployee>> GetAssignmentsByEmployeeAndPeriodAsync(Guid employeeId, DateTime start, DateTime end, bool includeDeleted = false);
    Task<(List<OrderEmployee> Items, int TotalCount)> GetPagedByEmployeeAsync(
        Guid employeeId,
        DateTime? start,
        DateTime? end,
        OrderStatus? status,
        int page,
        int pageSize,
        string? sortBy,
        bool sortDesc
    );
}
