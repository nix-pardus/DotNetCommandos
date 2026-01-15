using UI.Models.Employees;

namespace UI.Models;

public class AuthResponse
{
    public string Token { get; set; }
    public DateTime ExpiresAt { get; set; }
    public EmployeeMinimal Employee { get; set; }
}
