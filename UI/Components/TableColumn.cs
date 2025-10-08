namespace UI.Components;

public class TableColumn<TItem>
{
    public string Header { get; set; } = string.Empty;
    public Func<TItem, object?> ValueSelector { get; set; } = _ => string.Empty;
}
