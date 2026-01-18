namespace UI.Models;

public class PagingResponse<T>
{
    public int TotalCount { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalPages {  get; set; }
    public List<T> Items { get; set; }
}
