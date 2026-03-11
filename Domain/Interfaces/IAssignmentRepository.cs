using ServiceCenter.Domain.Entities;

namespace ServiceCenter.Domain.Interfaces;

public interface IAssignmentRepository : IRepository<OrderEmployee>
{
    Task<List<OrderEmployee>> GetConflictingAssignmentsAsync(Guid employeeId, DateTime start, DateTime end, Guid? excludeOrderId = null);
}
