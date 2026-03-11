namespace ServiceCenter.Application.DTO.Requests;

public class AssignmentConflictCheckRequest
{
    public List<Guid> EmployeeIds { get; set; } = new();
    public DateTime Start {  get; set; }
    public DateTime End { get; set; }
    public Guid? ExcludeOrderId { get; set; }
}
