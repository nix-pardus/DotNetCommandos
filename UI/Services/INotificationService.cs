namespace UI.Services;

public interface INotificationService
{
    void ShowError(string message, string title = "Ошибка");
    void ShowWarning(string message, string title = "Внимание");
    void ShowSuccess(string message);
    void ShowInfo(string message);
    Task ShowErrorDialog(string message, string title = "Ошибка", string? stackTrace = null);
    Task ShowGlobalError(string message);
}
