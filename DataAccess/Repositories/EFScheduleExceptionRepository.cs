using Microsoft.EntityFrameworkCore;
using ServiceCenter.Domain.Entities;
using ServiceCenter.Domain.Interfaces;

namespace ServiceCenter.Infrascructure.DataAccess.Repositories;

public class EFScheduleExceptionRepository(DataContext context) : IScheduleExceptionRepository
{
    private readonly DataContext _context = context;
    public async Task AddAsync(ScheduleException scheduleException)
    {
        _context.ScheduleExceptions.Add(scheduleException);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var schedule = await _context.ScheduleExceptions.SingleOrDefaultAsync(x => x.Id == id);
        if (schedule != null)
        {
            schedule.IsDeleted = true;
        }
        else
        {
            throw new ArgumentNullException(@$"Schedule Exception ""{id}"" is not exists");
        }
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<ScheduleException>> GetAllByIntervalAsync(DateOnly startDate, DateOnly endDate, Guid? employeeId = null)
    {
        return await _context.ScheduleExceptions.Where(x => employeeId != null ? (x.EmployeeId == employeeId && !x.IsDeleted) : true && ((x.EffectiveTo != null && x.EffectiveTo > startDate && x.EffectiveFrom <= endDate) || (x.EffectiveTo == null))).ToListAsync();
    }

    public async Task<IEnumerable<ScheduleException>> GetByEmployeePaged(Guid employeeId, int page, int pageSize)
    {
        return await _context.ScheduleExceptions.Where(x => x.EmployeeId == employeeId && !x.IsDeleted).Skip(pageSize * (page - 1)).Take(pageSize).ToListAsync();
    }

    public async Task<ScheduleException?> GetByIdAsync(Guid id)
    {
        return await _context.ScheduleExceptions.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
    }

    public async Task UpdateAsync(ScheduleException scheduleException)
    {
        _context.ScheduleExceptions.Update(scheduleException);
        await _context.SaveChangesAsync();
    }
}
