namespace UI.Models.Assignments;

public class AssignmentCreate
{
    public Guid OrderId { get; set; }
    public Guid EmployeeId { get; set; }
    public bool IsPrimary { get; set; }
}
