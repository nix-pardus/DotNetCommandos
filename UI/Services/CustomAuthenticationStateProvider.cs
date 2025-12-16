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
        var identity = string.IsNullOrEmpty(token)
            ? new ClaimsIdentity()
            : new ClaimsIdentity(new[] {new Claim("access_token", token)}, "jwt");

        return new AuthenticationState(new ClaimsPrincipal(identity));
    }
}
