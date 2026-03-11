namespace ServiceCenter.Application.DTO.Responses;

public class OrderConflictInfo
{
    public Guid OrderId { get; set; }
    public DateTime StartDateTime { get; set; }
    public DateTime EndDateTime { get; set; }
    public string ClientName { get; set; } = string.Empty;
}
