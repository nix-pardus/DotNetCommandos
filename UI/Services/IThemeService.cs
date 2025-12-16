using MudBlazor;

namespace UI.Services;

public interface IThemeService
{
    bool IsDarkMode { get; set; }
    MudTheme Theme { get; }
    Task InitializeAsync();
    Task ToggleThemeAsync(bool? newValue = null);
    Task<bool> LoadThemeFromStorageAsync();
}
