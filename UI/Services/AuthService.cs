using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;
using UI.Models;

namespace UI.Services;

public class AuthService : IAuthService
{
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _localStorage;
    private readonly AuthenticationStateProvider _authenticationStateProvider;

    public AuthService(HttpClient httpClient, ILocalStorageService localStorage)
    {
        _httpClient = httpClient;
        _localStorage = localStorage;
    }

    public async Task<string> GetJwtTokenAsync()
    {
        return await _localStorage.GetItemAsync<string>("authToken") ?? "";
    }

    public async Task<bool> IsAuthenticatedAsync()
    {
        var token = await _localStorage.GetItemAsync<string>("authToken");
        return !string.IsNullOrEmpty(token);
    }

    public async Task<EmployeeMinimal?> GetCurrentEmployeeAsync()
    {
        return await _localStorage.GetItemAsync<EmployeeMinimal>("employee");
    }

    public async Task<AuthResponse> LoginAsync(string email, string password)
    {
        var loginData = new {email, password};
        var response = await _httpClient.PostAsJsonAsync("api/Auth/login", loginData);

        if (!response.IsSuccessStatusCode)
            throw new Exception("Неудачная аутентификация.");

        var authResponse = await response.Content.ReadFromJsonAsync<AuthResponse>();
        if (authResponse == null)
            throw new Exception("Ответ от сервера не содержит данных.");

        await _localStorage.SetItemAsync("authToken", authResponse.Token);
        await _localStorage.SetItemAsync("employee", authResponse.Employee);

        await _localStorage.SetItemAsync("showWelcome", true);

        (_authenticationStateProvider as CustomAuthenticationStateProvider)?.NotifyAuthenticationStateChanged();

        return authResponse;
    }

    public async Task LogoutAsync()
    {
        await _localStorage.RemoveItemAsync("authToken");
        await _localStorage.RemoveItemAsync("employee");
        await _localStorage.RemoveItemAsync("showWelcome");

        (_authenticationStateProvider as CustomAuthenticationStateProvider)?.NotifyAuthenticationStateChanged();
    }

    public async Task<bool> IsWelcomeFlagSet()
    {
        return await _localStorage.GetItemAsync<bool>("showWelcome");
    }

    public async Task ClearWelcomeFlag()
    {
        await _localStorage.RemoveItemAsync("showWelcome");
    }
}