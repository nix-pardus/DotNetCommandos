using Microsoft.EntityFrameworkCore;
using ServiceCenter.Domain.Entities;
using ServiceCenter.Domain.Interfaces;

namespace ServiceCenter.Infrascructure.DataAccess.Repositories;

public class EFAssignmentRepository : BaseRepository<OrderEmployee>, IAssignmentRepository
{
    public EFAssignmentRepository(DataContext context, IFilterBuilder<OrderEmployee> filterBuilder) : base(context, filterBuilder)
    {
    }

    public async Task<List<OrderEmployee>> GetConflictingAssignmentsAsync(Guid employeeId, DateTime start, DateTime end, Guid? excludeOrderId = null)
    {
        var query = _context.OrderEmployees
            .Include(oe => oe.Order)
            .ThenInclude(o => o.Client)
            .Where(oe => oe.EmployeeId == employeeId
                && !oe.IsDeleted
                && oe.Order != null
                && !oe.Order.IsDeleted
                && oe.Order.StartDateTime.HasValue
                && oe.Order.EndDateTime.HasValue
                && oe.Order.StartDateTime < end
                && oe.Order.EndDateTime > start
            );

        if(excludeOrderId.HasValue)
        {
            query = query.Where(oe => oe.OrderId != excludeOrderId.Value);
        }

        return await query.ToListAsync();
    }
}
