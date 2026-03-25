using Microsoft.EntityFrameworkCore;
using ServiceCenter.Domain.Entities;
using ServiceCenter.Domain.Interfaces;

namespace ServiceCenter.Infrascructure.DataAccess.Repositories;

public class EFScheduleRepository(DataContext context) : IScheduleRepository
{
    private readonly DataContext _context = context;
    public async Task AddAsync(Schedule schedule)
    {
        _context.Schedules.Add(schedule);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var schedule = await _context.Schedules.SingleOrDefaultAsync(x => x.Id == id);
        if (schedule != null)
        {
            schedule.IsDeleted = true;
        }
        else
        {
            throw new ArgumentNullException(@$"Schedule ""{id}"" is not exists");
        }
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Schedule>> GetAllByIntervalAsync(DateOnly startDate, DateOnly endDate, Guid? employeeId = null)
    {
        var query = _context.Schedules.Where(x => !x.IsDeleted);

        if(employeeId.HasValue)
            query = query.Where(x => x.EmployeeId == employeeId.Value);

        query = query.Where(x =>
        (x.EffectiveTo != null && x.EffectiveTo > startDate && x.EffectiveFrom <= endDate) ||
        (x.EffectiveTo == null));

        return await query.ToListAsync();
    }

    public async Task UpdateAsync(Schedule schedule)
    {
        _context.Schedules.Update(schedule);
        await _context.SaveChangesAsync();
    }
}
