
using Microsoft.JSInterop;
using MudBlazor;
using UI.Components;

namespace UI.Services;

public class NotificationService : INotificationService
{
    private readonly ISnackbar _snackbar;
    private readonly IDialogService _dialogService;
    private readonly IJSRuntime _jSRuntime;

    public NotificationService(ISnackbar snackbar, IDialogService dialogService, IJSRuntime jSRuntime)
    {
        _snackbar = snackbar;
        _dialogService = dialogService;
        _jSRuntime = jSRuntime;
    }

    public void ShowError(string message, string title = "Ошибка")
    {
        _snackbar.Add($"{title}: {message}", Severity.Error, config =>
        {
            config.RequireInteraction = true;

        });
    }

    public async Task ShowErrorDialog(string message, string title = "Ошибка", string? stackTrace = null)
    {
        var parameters = new DialogParameters
        {
            [nameof(ErrorDialog.Title)] = title,
            [nameof(ErrorDialog.Message)] = message,
            [nameof(ErrorDialog.StackTrace)] = stackTrace
        };

        var options = new DialogOptions
        {
            CloseButton = true,
            MaxWidth = MaxWidth.Medium,
            CloseOnEscapeKey = true,
            FullWidth = true
        };

        await _dialogService.ShowAsync<ErrorDialog>(title, parameters, options);
    }

    public async Task ShowGlobalError(string message)//под вопросом
    {
        _snackbar.Add(message, Severity.Error, config =>
        {
            config.VisibleStateDuration = 10000;
            config.HideTransitionDuration = 500;
            config.ShowTransitionDuration = 500;
        });

        await _jSRuntime.InvokeVoidAsync("console.error", message);
    }

    public void ShowInfo(string message)
    {
        _snackbar.Add(message, Severity.Info);
    }

    public void ShowSuccess(string message)
    {
        _snackbar.Add(message, Severity.Success);
    }

    public void ShowWarning(string message, string title = "Внимание")
    {
        _snackbar.Add($"{title}: {message}", Severity.Warning, config =>
        {
            config.RequireInteraction = true;
        });
    }
}
