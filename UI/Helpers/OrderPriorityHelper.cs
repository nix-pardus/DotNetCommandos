using MudBlazor;

namespace UI.Helpers;

public static class OrderPriorityHelper
{
    public static string OrderPriorityToString(this int priority)
    {
        return priority switch
        {
            0 => "Низкий",
            1 => "Обычный",
            2 => "Высокий",
            3 => "Критический",
            _ => "-"
        };
    }

    public static Color OrderPriorityToColor(this int priority)
    {
        return priority switch
        {
            0 => Color.Success,
            1 => Color.Info,
            2 => Color.Warning,
            3 => Color.Error,
            _ => Color.Default
        };
    }
}
