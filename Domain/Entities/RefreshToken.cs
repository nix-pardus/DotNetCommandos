namespace ServiceCenter.Domain.Entities;

public class RefreshToken
{
    public Guid Id { get; set; }
    public string Token { get; set; } = string.Empty;
    public Guid EmployeeId { get; set; }
    // Дата истечения
    public DateTime ExpiryDate { get; set; }
    public DateTime CreatedAt { get; set; }
    // Дата отзыва
    public DateTime? RevokedAt { get; set; }

    public Employee Employee { get; set; } = null!;
}
