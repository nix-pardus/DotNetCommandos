using MudBlazor;

namespace UI.Models;

public class GetByFiltersRequest
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string? LogicalOperator { get; set; }
    public List<Sorting> SortBy { get; set; } = new();
    public List<FilterCondition> Filters { get; set; } = new();

    public class Sorting
    {
        public string Field { get; set; }
        public bool Direction { get; set; }
    }

    public class FilterCondition
    {
        public string Field { get; set; }
        public FilterOperator Operator { get; set; }
        public string Value { get; set; }
        public string ValueType { get; set; }
    }
    public enum FilterOperator
    {
        Equals,
        NotEquals,
        Contains,
        StartsWith, //не реалзован
        EndsWith, //не реалзован
        GreaterThan,
        LessThan,
        GreaterThanOrEqual, //не реалзован
        LessThanOrEqual, //не реалзован
        In,//я не хочу его делать=(
        IsNull,
        IsNotNull
    }
}