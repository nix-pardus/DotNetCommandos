using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace UI.Services;

public class CustomAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly IAuthService _authService;

    public CustomAuthenticationStateProvider(IAuthService authService)
    {
        _authService = authService;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var token = await _authService.GetJwtTokenAsync();
        var employee = await _authService.GetCurrentEmployeeAsync();

        if (string.IsNullOrEmpty(token) || employee == null)
        {
            return new AuthenticationState(new ClaimsPrincipal());
        }

        var claims = new[]
        {
            new Claim("access_token", token),
            new Claim(ClaimTypes.Name, $"{employee.Name} {employee.LastName}"),
            new Claim("UserId", employee.Id.ToString())
        };

        var identity = new ClaimsIdentity(claims, "jwt");

        return new AuthenticationState(new ClaimsPrincipal(identity));
    }

    public void NotifyAuthenticationStateChanged()
    {
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }
}
