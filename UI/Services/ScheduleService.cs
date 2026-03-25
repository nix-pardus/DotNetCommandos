using UI.Models.Schedule;

namespace UI.Services;

public class ScheduleService : IScheduleService
{
    private readonly IApiService _apiService;

    public ScheduleService(IApiService apiService)
    {
        _apiService = apiService;
    }
    public async Task<List<ScheduleDayDto>> GetScheduleByEmployeeAsync(Guid employeeId, DateOnly startDate, DateOnly endDate)
    {
        var endpoint = $"api/Schedule/get-employee-work-days-by-interval?employeeId={employeeId}&startDate={startDate:yyyy-MM-dd}&endDate={endDate:yyyy-MM-dd}";
        return await _apiService.GetFromJsonAsync<List<ScheduleDayDto>>(endpoint);
    }
    public async Task<List<Schedule>> GetSchedulesByEmployeeAsync(Guid employeeId)
    {
        return await _apiService.GetFromJsonAsync<List<Schedule>>($"api/Schedule/by-employee?employeeId={employeeId}");
    }

    public async Task<Schedule> CreateScheduleAsync(ScheduleCreate schedule)
    {
        await _apiService.PostAsync("api/Schedule/create", schedule);
        return null;
    }

    public async Task<Schedule> UpdateScheduleAsync(ScheduleUpdate schedule)
    {
        await _apiService.PutAsync($"api/Schedule/update?id={schedule.Id}", schedule);
        return null;
    }

    public async Task DeleteScheduleAsync(Guid id)
    {
        await _apiService.DeleteAsync($"api/Schedule/delete?id={id}");
    }
}
