using Microsoft.EntityFrameworkCore;
using ServiceCenter.Domain.Entities;
using ServiceCenter.Domain.Interfaces;
using ServiceCenter.Domain.ValueObjects.Enums;

namespace ServiceCenter.Infrascructure.DataAccess.Repositories;

public class EFAssignmentRepository : BaseRepository<OrderEmployee>, IAssignmentRepository
{
    public EFAssignmentRepository(DataContext context, IFilterBuilder<OrderEmployee> filterBuilder) : base(context, filterBuilder)
    {
    }

    public async Task<List<OrderEmployee>> GetAssignmentsByEmployeeAndPeriodAsync(Guid employeeId, DateTime start, DateTime end, bool includeDeleted = false)
    {
        var query = _context.OrderEmployees
            .Include(oe => oe.Order)
            .ThenInclude(o => o.Client)
            .Where(oe => oe.EmployeeId == employeeId);

        if (!includeDeleted)
        {
            query = query.Where(oe => !oe.IsDeleted && (oe.Order == null || !oe.Order.IsDeleted));
        }

        query = query.Where(oe => oe.Order != null
        && oe.Order.StartDateTime.HasValue
        && oe.Order.EndDateTime.HasValue
        && oe.Order.StartDateTime < end
        && oe.Order.EndDateTime > start);

        return await query.ToListAsync();
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

        if (excludeOrderId.HasValue)
        {
            query = query.Where(oe => oe.OrderId != excludeOrderId.Value);
        }

        return await query.ToListAsync();
    }

    public async Task<(List<OrderEmployee> Items, int TotalCount)> GetPagedByEmployeeAsync(Guid employeeId, DateTime? start, DateTime? end, OrderStatus? status, int page, int pageSize, string? sortBy, bool sortDesc)
    {
        var query = _context.OrderEmployees
            .Include(oe => oe.Order)
            .ThenInclude(o => o.Client)
            .Where(oe => oe.EmployeeId == employeeId && !oe.IsDeleted && (oe.Order != null && !oe.Order.IsDeleted));

        if (start.HasValue)
        {
            query = query.Where(oe => oe.Order.StartDateTime >= start.Value);
        }
        if (end.HasValue)
        {
            query = query.Where(oe => oe.Order.EndDateTime <= end.Value);
        }
        if (status.HasValue)
        {
            query = query.Where(oe => oe.Order.Status == status.Value);
        }

        var totalCount = await query.CountAsync();

        if (!string.IsNullOrEmpty(sortBy))
        {
            query = sortDesc
                ? query.OrderByDescending(e => EF.Property<object>(e.Order, sortBy))
                : query.OrderBy(e => EF.Property<object>(e.Order, sortBy));
        }
        else
        {
            query = query.OrderByDescending(oe => oe.Order.StartDateTime);
        }

        var items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (items, totalCount);
    }
}
