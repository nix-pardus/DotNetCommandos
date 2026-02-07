namespace UI.Models.Orders;

public class Order
{
    public Guid Id { get; set; }
    public Guid CreatedById { get; set; }
    public Guid? ModifiedById { get; set; }
    public Guid ClientId { get; set; }
    public string? EquipmentType { get; set; }
    public string? EquipmentModel { get; set; }
    public bool IsWarranty { get; set; }
    public string Problem { get; set; }
    public string? Comment { get; set; }
    public int Priority { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? EndDateTime { get; set; }
    public DateTime? StartDateTime { get; set; }
    public OrderStatus Status { get; set; }
}
