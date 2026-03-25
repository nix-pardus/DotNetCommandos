using UI.Models.ScheduleExceptions;

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

    public async Task<List<ScheduleException>> GetScheduleExceptionsByEmployeeAsync(Guid employeeId)
    {
        return await apiService.GetFromJsonAsync<List<ScheduleException>>($"api/ScheduleException/get-schedule-exceptions-by-interval?employeeId={employeeId}&page=1&pageSize=100");
    }
}
