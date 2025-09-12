using ServiceCenter.Domain.ValueObjects.Enums;

namespace ServiceCenter.Domain.Queries;

public class GetEmployeesQuery
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string? Name { get; set; }
    public string? LastName { get; set; }
    public string? Patronymic { get; set; }
    public string? Address { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public RoleType? Role { get; set; }
    public Guid? CreatedById { get; set; }
    public DateTime? CreatedDateAfter { get; set; }
    public DateTime? CreatedDateBefore { get; set; }
    public bool? IsDeleted { get; set; }
    public EmployeeSortBy SortBy { get; set; } = EmployeeSortBy.FullName;
    public bool IsSortDescending { get; set; } = false;
}
