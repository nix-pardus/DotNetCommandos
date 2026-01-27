namespace UI.Models.Clients;

public class ClientUpdate
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? Patronymic { get; set; }
    public string Address { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string? Region { get; set; }
    public string? CompanyName { get; set; }
    public string? Email { get; set; }
    public string PhoneNumber { get; set; } = string.Empty;
    public bool IsDeleted { get; set; }
}