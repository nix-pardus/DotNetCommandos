using Microsoft.JSInterop;
using MudBlazor;

namespace UI.Services;

public class ThemeService : IThemeService
{
    private readonly IJSRuntime _jSRuntime;
    private bool _isDarkMode;
    private readonly MudTheme _theme = new();

    public bool IsDarkMode
    {
        get { return _isDarkMode; } 
        set { _isDarkMode = value; }
    }
    public MudTheme Theme => _theme;

    public ThemeService(IJSRuntime jSRuntime)
    {
        _jSRuntime = jSRuntime;
    }

    public async Task InitializeAsync()
    {
        await LoadThemeFromStorageAsync();
    }

    public async Task<bool> LoadThemeFromStorageAsync()
    {
        try
        {
            var themeString = await _jSRuntime.InvokeAsync<string>(
                "localStorage.getItem", "preferredTheme");

            if (!string.IsNullOrEmpty(themeString))
            {
                _isDarkMode = themeString.ToLower() == "true";
                return true;
            }
        }
        catch(Exception ex)
        {
            Console.WriteLine($"Ошибка при загрузке темы: {ex.Message}");
        }
        return false;
    }

    public async Task ToggleThemeAsync(bool? newValue = null)
    {
        _isDarkMode = newValue ?? !_isDarkMode;
        await _jSRuntime.InvokeVoidAsync(
            "localStorage.setItem", "preferredTheme", _isDarkMode.ToString());
    }
}
