namespace UI.Models.Schedule;

public class Schedule
{
    public Guid Id { get; set; }
    public string Pattern { get; set; } = string.Empty; // например: "5/2"
    public DateOnly EffectiveFrom { get; set; }
    public DateOnly? EffectiveTo { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public Guid EmployeeId { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public bool IsDeleted { get; set; }
}
