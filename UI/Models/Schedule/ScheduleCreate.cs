namespace UI.Models.Schedule;

public class ScheduleCreate
{
    public string Pattern { get; set; } = string.Empty;
    public DateOnly EffectiveFrom { get; set; }
    public DateOnly? EffectiveTo { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public Guid EmployeeId { get; set; }
}
