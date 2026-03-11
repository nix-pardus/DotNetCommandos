using UI.Models.Shedule;

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
}
