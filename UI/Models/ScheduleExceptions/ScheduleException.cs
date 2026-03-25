namespace UI.Models.ScheduleExceptions;

public class ScheduleException
{
    public Guid Id { get; set; }
    public ScheduleExceptionType? ExceptionType { get; set; }
    public DateOnly EffectiveFrom { get; set; }
    public DateOnly EffectiveTo { get; set; }
    public Guid EmployeeId { get; set; }
    public bool IsDeleted { get; set; }
}
