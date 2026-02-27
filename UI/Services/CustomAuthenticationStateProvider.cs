using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Json;
using System.Security.Claims;
using UI.Models;
using UI.Models.Employees;

namespace UI.Services;

public class CustomAuthenticationStateProvider : AuthenticationStateProvider, IAuthenticationStateNotifier
{
    private readonly ILocalStorageService _localStorage;
    private readonly HttpClient _httpClient;

    public CustomAuthenticationStateProvider(IHttpClientFactory httpClientFactory,  ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
        _httpClient = httpClientFactory.CreateClient("PublicClient");
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var token = await _localStorage.GetItemAsync<string>("authToken");
        var employee = await _localStorage.GetItemAsync<EmployeeMinimal>("employee");

        if (string.IsNullOrEmpty(token) || employee == null)
        {
            return new AuthenticationState(new ClaimsPrincipal());
        }

        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);
        var expClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "exp")?.Value;
        if (expClaim != null)
        {
            var exp = DateTimeOffset.FromUnixTimeSeconds(long.Parse(expClaim)).UtcDateTime;
            if (exp < DateTime.UtcNow)
            {
                var refreshed = await RefreshTokenAsync();
                if (!refreshed)
                {
                    await _localStorage.RemoveItemAsync("authToken");
                    await _localStorage.RemoveItemAsync("employee");
                    return new AuthenticationState(new ClaimsPrincipal());
                }

                token = await _localStorage.GetItemAsync<string>("authToken");
                employee = await _localStorage.GetItemAsync<EmployeeMinimal>("employee");
            }
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

    private async Task<bool> RefreshTokenAsync()
    {
        try
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "api/Auth/refresh");
            request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

            var response = await _httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
                return false;

            var result = await response.Content.ReadFromJsonAsync<AuthResponse>();
            if (result == null)
                return false;

            await _localStorage.SetItemAsync("authToken", result.Token);
            await _localStorage.SetItemAsync("employee", result.Employee);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public void NotifyStateChanged()
    {
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }
}
