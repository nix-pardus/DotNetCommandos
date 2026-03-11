using UI.Models.Shedule;

namespace UI.Services;

public interface IScheduleService
{
    Task<List<ScheduleDayDto>> GetScheduleByEmployeeAsync(Guid employeeId, DateOnly startDate, DateOnly endDate);
}
