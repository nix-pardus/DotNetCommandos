using ServiceCenter.Domain.Entities;
using ServiceCenter.Domain.ValueObjects.Enums;

namespace ServiceCenter.Infrascructure.DataAccess.Repositories.Employees;

public static class EmployeeSortHelper
{
    public static IQueryable<Employee> ApplySorting(IQueryable<Employee> query, EmployeeSortBy sortBy, bool descending)
    {
        return sortBy switch
        {
            EmployeeSortBy.CreatedDate => descending
                ? query.OrderByDescending(x => x.CreatedDate)
                : query.OrderBy(x => x.CreatedDate),

            EmployeeSortBy.ModifiedDate => descending
                ? query.OrderByDescending(x => x.ModifiedDate)
                : query.OrderBy(x => x.ModifiedDate),

            EmployeeSortBy.FullName => descending
                ? query
                    .OrderByDescending(x => x.LastName)
                    .ThenByDescending(x => x.Name)
                    .ThenByDescending(x => x.Patronymic)
                : query
                    .OrderBy(x => x.LastName)
                    .ThenBy(x => x.Name)
                    .ThenBy(x => x.Patronymic),

            EmployeeSortBy.Role => descending
                ? query.OrderByDescending(x => x.Role)
                : query.OrderBy(x => x.Role),

            _ => descending
                ? query
                    .OrderByDescending(x => x.LastName)
                    .ThenByDescending(x => x.Name)
                    .ThenByDescending(x => x.Patronymic)
                : query
                    .OrderBy(x => x.LastName)
                    .ThenBy(x => x.Name)
                    .ThenBy(x => x.Patronymic)
        };
    }
}
