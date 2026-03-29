using MudBlazor;
using UI.Models;

namespace UI.Helpers;

public static class OrderStatusHelper
{
    public static string ToDisplayString(this OrderStatus orderStatus)
    {
        return orderStatus switch
        {
            OrderStatus.Canceled => "Отменено",
            OrderStatus.Created => "Создано",
            OrderStatus.Done => "Завершено",
            OrderStatus.Planned => "Запланировано",
            OrderStatus.Processing => "Выполняется",
            _ => orderStatus.ToString()
        };
    }

    public static Color GetColor(this OrderStatus orderStatus)
    {
        return orderStatus switch
        {
            OrderStatus.Canceled => Color.Error,
            OrderStatus.Created => Color.Info,
            OrderStatus.Done => Color.Success,
            OrderStatus.Planned => Color.Default,
            OrderStatus.Processing => Color.Primary,
            _ => Color.Default
        };
    }

    public static string GetIcon(this OrderStatus orderStatus)
    {
        return orderStatus switch
        {
            OrderStatus.Canceled => Icons.Material.Filled.Cancel,
            OrderStatus.Created => Icons.Material.Filled.Create,
            OrderStatus.Done => Icons.Material.Filled.Done,
            OrderStatus.Planned => Icons.Material.Filled.NextPlan,
            OrderStatus.Processing => Icons.Material.Filled.RunCircle,
            _ => Icons.Material.Filled.Help
        };
    }
}
