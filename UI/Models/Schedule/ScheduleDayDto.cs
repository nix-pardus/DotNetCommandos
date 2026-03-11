namespace UI.Models.Shedule;

public class ScheduleDayDto
{
    public string DayType { get; set; } = string.Empty;
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public DateOnly Date { get; set; }
}
