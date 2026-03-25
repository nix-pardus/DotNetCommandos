namespace UI.Models.Schedule;

public class ScheduleUpdate
{
    public Guid Id { get; set; }
    public string Pattern { get; set; } = string.Empty;
    public DateOnly EffectiveFrom { get; set; }
    public DateOnly? EffectiveTo { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public Guid EmployeeId { get; set; }
    public bool IsDeleted { get; set; }
}
