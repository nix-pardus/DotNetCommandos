using UI.Models.Schedule;

namespace UI.Services;

public class ScheduleExceptionService(IApiService apiService) : IScheduleExceptionService
{
    public async Task CreateScheduleExceptionAsync(ScheduleExceptionCreate exception)
    {
        await apiService.PostAsync("api/ScheduleException/create", exception);
    }

    public async Task DeleteScheduleExceptionAsync(Guid id)
    {
        await apiService.DeleteAsync($"api/ScheduleException/delete?id={id}");
    }
}
