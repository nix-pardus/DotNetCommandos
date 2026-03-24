namespace UI.Models.Schedule;

public class ScheduleExceptionCreate
{
    public ScheduleExceptionType? ExceptionType { get; set; }
    public DateOnly EffectiveFrom { get; set; }
    public DateOnly EffectiveTo { get; set; }
    public Guid EmployeeId { get; set; }
}
