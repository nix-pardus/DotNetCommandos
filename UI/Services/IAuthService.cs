using UI.Models;

namespace UI.Services;

public interface IAuthService
{
    Task<AuthResponse> LoginAsync(string email, string password);
    Task<bool> IsAuthenticatedAsync();
    Task<string> GetJwtTokenAsync();
    Task LogoutAsync();
    Task<EmployeeMinimal?> GetCurrentEmployeeAsync();

    Task<bool> IsWelcomeFlagSet();
    Task ClearWelcomeFlag();
}
