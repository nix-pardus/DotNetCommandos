using Microsoft.AspNetCore.Components;
using MudBlazor;
using UI.Components;

namespace UI.Services;

public static class DialogServiceExtensions
{
    public static async Task<bool> ShowDeleteDialog<T>(
        this IDialogService dialogService,
        T item,
        string? title = null,
        Func<T, string>? getDisplayName = null,
        RenderFragment<T>? confirmationTemplate = null,
        Func<T, Task<bool>>? deleteAction = null)
    {
        var parameters = new DialogParameters
        {
            {nameof(DeleteDialog<T>.Item), item },
            {nameof(DeleteDialog<T>.Title), title ?? "Подтверждение удаления" },
            {nameof(DeleteDialog<T>.GetDisplayName), getDisplayName },
            {nameof(DeleteDialog<T>.ConfirmationTemplate), confirmationTemplate },
            {nameof(DeleteDialog<T>.OnDelete), deleteAction }
        };

        var options = new DialogOptions
        {
            CloseButton = true,
            MaxWidth = MaxWidth.Small,
            CloseOnEscapeKey = true,
            NoHeader = true
        };

        var dialog = await dialogService.ShowAsync<DeleteDialog<T>>(
            title ?? "Подтверждение удаления",
            parameters,
            options);


        var result = await dialog.Result; // Асинхронное ожидание результата

        // Возвращаем true, если диалог не был отменен
        return !result.Canceled && result.Data is bool success && success;
    }
}
