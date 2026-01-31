using Microsoft.JSInterop;
using System.Text.Json;

namespace UI.Services;

public class UserPreferencesService : IUserPreferencesService
{
    private readonly IJSRuntime _jsRuntime;
    private const string StoragePrefix = "dotNetCommandos_";
    public UserPreferencesService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }
    public async Task<T?> GetPreferenceAsync<T>(string key)
    {
        try
        {
            var json = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", StoragePrefix + key);

            if(string.IsNullOrEmpty(json))
            {
                return default;
            }

            return JsonSerializer.Deserialize<T>(json);
        }
        catch
        {
            return default;
        }
    }

    public async Task RemovePreferenceAsync(string key)
    {
        try
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", StoragePrefix + key);
        }
        catch
        {
            // Ignore errors
        }
    }

    public async Task SetPreferenceAsync<T>(string key, T value)
    {
        try
        {
            var json = JsonSerializer.Serialize(value);
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", StoragePrefix + key, json);
        }
        catch
        {
            // Ignore errors
        }
    }
}
