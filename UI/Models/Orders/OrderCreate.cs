namespace UI.Models.Orders;

public class OrderCreate
{
    public Guid ClientId { get; set; }
    public string? EquipmentType { get; set; }
    public string? EquipmentModel { get; set; }
    public bool IsWarranty { get; set; }
    public string Problem { get; set; }
    public string? Comment { get; set; }
    public int Priority { get; set; }
    public DateTime? EndDateTime { get; set; }
    public DateTime? StartDateTime { get; set; }
}
