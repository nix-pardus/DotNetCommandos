
using Microsoft.JSInterop;

namespace UI.Services;

public class ErrorHandler : IErrorHandler, IDisposable
{
    private readonly IJSRuntime _jSRuntime;
    private readonly INotificationService _notificationService;
    private DotNetObjectReference<ErrorHandler> _dotNetObjectReference;
    private bool _errorHandlingInitialized = false;

    public ErrorHandler(IJSRuntime jSRuntime, INotificationService notificationService)
    {
        _jSRuntime = jSRuntime;
        _notificationService = notificationService;
    }

    public async Task InitializeAsync()
    {
        if (_errorHandlingInitialized) return;

        try
        {
            _dotNetObjectReference = DotNetObjectReference.Create(this);

            try
            {
                var isFunctionDefined = await _jSRuntime.InvokeAsync<bool>(
                    "eval", "typeof setupErrorHandlers === 'function'");

                if(isFunctionDefined)
                {
                    await _jSRuntime.InvokeVoidAsync("setupErrorHandlers", _dotNetObjectReference);
                    _errorHandlingInitialized = true;
                }
                else
                {
                    await InitializeFallbackErrorHandlingAsync();
                }
            }
            catch(JSException jsEx)
            {
                Console.WriteLine($"Ошибка при проверке функции: {jsEx.Message}");
                await InitializeFallbackErrorHandlingAsync();
            }
        }
        catch(Exception ex)
        {
            Console.WriteLine($"Ошибка при инициализации обработки ошибок: {ex.Message}");
        }
    }

    private async Task InitializeFallbackErrorHandlingAsync()
    {
        try
        {
            await _jSRuntime.InvokeVoidAsync("eval",
                @"window.addEventListener('error', function(e) {
                    console.error('Fallback error handler:', e.error);
                    if (window.dotNetHelper) {
                        window.dotNetHelper.invokeMethodAsync('HandleGlobalError',
                            e.error.message, e.error.stack);
                    }
                });");

            _errorHandlingInitialized = true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при альтернативной инициализации: {ex.Message}");
        }
    }

    [JSInvokable]
    public async Task HandleGlobalErrorAsync(string errorMessage, string? stackTrace = null)
    {
        try
        {
            var errorType = GetErrorType(errorMessage);

            if (IsCriticalError(errorMessage))
            {
                await _notificationService.ShowErrorDialog(errorMessage, "Критическая ошибка", stackTrace);
            }
            else
            {
                await _notificationService.ShowGlobalError($"{errorType}: {errorMessage}");
            }

            var logMessage = $"[{DateTime.Now:HH:mm:ss}] {errorType}: {errorMessage}";
            if (!string.IsNullOrEmpty(stackTrace))
                logMessage += $"\nStack trace: {stackTrace}";

            await _jSRuntime.InvokeVoidAsync("console.error", logMessage);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при обработке глобальной ошибки: {ex.Message}");
        }
    }

    private string GetErrorType(string errorMessage)
    {
        if (errorMessage.Contains("Blazor", StringComparison.OrdinalIgnoreCase) ||
            errorMessage.Contains("соединен", StringComparison.OrdinalIgnoreCase))
            return "Ошибка соединения";
        if (errorMessage.Contains("JavaScript", StringComparison.OrdinalIgnoreCase))
            return "JavaScript ошибка";
        if (errorMessage.Contains("promise", StringComparison.OrdinalIgnoreCase))
            return "Ошибка промиса";

        return "Ошибка";
    }

    private bool IsCriticalError(string errorMessage)
    {
        var criticalKeywords = new[]
        {
            "connection", "соединен", "network", "сеть",
            "failed to fetch", "не удалось", "timeout", "таймаут"
        };

        return criticalKeywords.Any(keyword =>
            errorMessage.Contains(keyword, StringComparison.OrdinalIgnoreCase));
    }

    public void Dispose()
    {
        _dotNetObjectReference?.Dispose();
    }
}
