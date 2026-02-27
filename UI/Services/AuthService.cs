using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using System.Net.Http.Json;
using UI.Models;
using UI.Models.Employees;

namespace UI.Services;

public class AuthService : IAuthService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILocalStorageService _localStorage;
    private readonly IAuthenticationStateNotifier _authenticationStateNotifier;

    public AuthService(IHttpClientFactory httpClientFactory, ILocalStorageService localStorage, IAuthenticationStateNotifier authenticationStateNotifier)
    {
        _httpClientFactory = httpClientFactory;
        _localStorage = localStorage;
        _authenticationStateNotifier = authenticationStateNotifier;
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
        var publicClient = _httpClientFactory.CreateClient("PublicClient");

        var request = new HttpRequestMessage(HttpMethod.Post, "api/Auth/login")
        {
            Content = JsonContent.Create(loginData)
        };
        request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

        var response = await publicClient.SendAsync(request);

        if (!response.IsSuccessStatusCode)
            throw new Exception("Неудачная аутентификация.");

        var authResponse = await response.Content.ReadFromJsonAsync<AuthResponse>();
        if (authResponse == null)
            throw new Exception("Ответ от сервера не содержит данных.");

        await _localStorage.SetItemAsync("authToken", authResponse.Token);
        await _localStorage.SetItemAsync("employee", authResponse.Employee);

        await _localStorage.SetItemAsync("showWelcome", true);

        _authenticationStateNotifier.NotifyStateChanged();

        return authResponse;
    }

    public async Task<bool> RefreshTokenAsync()
    {
        try
        {
            var publicClient = _httpClientFactory.CreateClient("PublicClient");

            var request = new HttpRequestMessage(HttpMethod.Post, "api/Auth/refresh");
            request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

            var response = await publicClient.SendAsync(request);
            if(!response.IsSuccessStatusCode)
            {
                await LogoutAsync();
                return false;
            }

            var result = await response.Content.ReadFromJsonAsync<AuthResponse>();
            if(result == null)
            {
                await LogoutAsync();
                return false;
            }

            await _localStorage.SetItemAsync("authToken", result.Token);
            await _localStorage.SetItemAsync("employee", result.Employee);
            return true;
        }
        catch
        {
            await LogoutAsync();
            return false;
        }
    }

    public async Task LogoutAsync()
    {
        await _localStorage.RemoveItemAsync("authToken");
        await _localStorage.RemoveItemAsync("employee");
        await _localStorage.RemoveItemAsync("showWelcome");

        try
        {
            var authorizedClient = _httpClientFactory.CreateClient("AuthorizedClient");
            await authorizedClient.PostAsync("api/Auth/logout", null);
        }
        catch { }

        _authenticationStateNotifier.NotifyStateChanged();
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