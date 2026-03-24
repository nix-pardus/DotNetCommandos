using UI.Models.Schedule;

namespace UI.Services;

public interface IScheduleExceptionService
{
    Task CreateScheduleExceptionAsync(ScheduleExceptionCreate exception);
    Task DeleteScheduleExceptionAsync(Guid id);
}
