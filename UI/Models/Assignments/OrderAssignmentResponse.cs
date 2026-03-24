using UI.Models.Orders;

namespace UI.Models.Assignments;

public class OrderAssignmentResponse
{
    public Guid Id { get; set; }
    public Order Order { get; set; } = null!;
    public bool IsPrimary { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public bool IsDeleted { get; set; }
    public Guid CreatedById { get; set; }
    public Guid? ModifiedById { get; set; }
}
