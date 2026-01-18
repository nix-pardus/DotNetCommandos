namespace UI.Services;

public interface IErrorHandler
{
    Task InitializeAsync();
    Task HandleGlobalErrorAsync(string errorMessage, string? stackTrace = null);
}
