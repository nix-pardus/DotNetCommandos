namespace UI.Models.Clients;

public class Client
{
    public Guid Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public bool IsDeleted { get; set; }
    public string Name { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? Patronymic { get; set; }
    public string Address { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string? Region { get; set; }
    public string? CompanyName { get; set; }
    public string? Email { get; set; }
    public string PhoneNumber { get; set; } = string.Empty;
    public Guid CreatedById { get; set; }
    public Guid? ModifiedById { get; set; }
    public string FullName => $"{LastName} {Name} {Patronymic}".Trim();
}