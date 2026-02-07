namespace UI.Models.Assignments;

public class Assignment
{
    public Guid Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public bool IsDeleted { get; set; }
    public Guid OrderId { get; set; }
    public Guid EmployeeId { get; set; }
    public bool IsPrimary { get; set; }
    public Guid CreatedById { get; set; }
    public Guid? ModifiedById { get; set; }
}
