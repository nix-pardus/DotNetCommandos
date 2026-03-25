using UI.Models.Schedule;

namespace UI.Services;

public interface IScheduleService
{
    Task<List<ScheduleDayDto>> GetScheduleByEmployeeAsync(Guid employeeId, DateOnly startDate, DateOnly endDate);
    Task<List<Schedule>> GetSchedulesByEmployeeAsync(Guid employeeId);
    Task<Schedule> CreateScheduleAsync(ScheduleCreate schedule);
    Task<Schedule> UpdateScheduleAsync(ScheduleUpdate schedule);
    Task DeleteScheduleAsync(Guid id);
}
