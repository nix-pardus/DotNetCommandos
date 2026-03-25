using UI.Models.ScheduleExceptions;

namespace UI.Services;

public interface IScheduleExceptionService
{
    Task CreateScheduleExceptionAsync(ScheduleExceptionCreate exception);
    Task DeleteScheduleExceptionAsync(Guid id);
    Task<List<ScheduleException>> GetScheduleExceptionsByEmployeeAsync(Guid employeeId);
}
