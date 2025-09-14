using Microsoft.EntityFrameworkCore;
using ServiceCenter.Domain.Entities;
using ServiceCenter.Domain.Interfaces;
using ServiceCenter.Domain.Queries;
using ServiceCenter.Domain.ValueObjects.Enums;
using System.Linq.Expressions;

namespace ServiceCenter.Infrascructure.DataAccess.Repositories;

public class EFEmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
{
    public EFEmployeeRepository(DataContext context) : base(context) { }

    public async Task AddAsync(Employee employee)
    {
        _context.Employees.Add(employee);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var employee = await _context.Employees.SingleOrDefaultAsync(x => x.Id == id);
        if (employee != null)
        {
            employee.IsDeleted = true;
            await _context.SaveChangesAsync();
        }
        else
        {
            throw new InvalidOperationException("Employee is not exist.");
        }
    }

    public async Task<IEnumerable<Employee>> GetAllActiveAsync()
    {
        return await _context.Employees
            .Where(x => x.IsDeleted == false)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<IEnumerable<Employee>> GetAllAsync()
    {
        return await _context.Employees
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Employee> GetByIdAsync(Guid id)
    {
        return await _context.Employees.SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task<(IEnumerable<Employee> Employees, int TotalCount)> GetEmployeesAsync(GetEmployeesQuery geq)
    {
        var query = _context.Employees.AsQueryable();

        query = ApplyFiltering(query, x =>
            (string.IsNullOrEmpty(geq.Name) || x.Name.Contains(geq.Name)) &&
            (string.IsNullOrEmpty(geq.LastName) || x.LastName.Contains(geq.LastName)) &&
            (string.IsNullOrEmpty(geq.Patronymic) || x.Patronymic.Contains(geq.Patronymic)) &&
            (string.IsNullOrEmpty(geq.Address) || x.Address.Contains(geq.Address)) &&
            (string.IsNullOrEmpty(geq.Email) || x.Email.Contains(geq.Email)) &&
            (string.IsNullOrEmpty(geq.PhoneNumber) || x.PhoneNumber.Contains(geq.PhoneNumber)) &&
            (!geq.Role.HasValue || x.Role == geq.Role.Value) &&
            (!geq.CreatedById.HasValue || x.CreatedById == geq.CreatedById.Value) &&
            (!geq.CreatedDateAfter.HasValue || x.CreatedDate >= geq.CreatedDateAfter.Value) &&
            (!geq.CreatedDateBefore.HasValue || x.CreatedDate <= geq.CreatedDateBefore.Value) &&
            (!geq.IsDeleted.HasValue || x.IsDeleted == geq.IsDeleted.Value)
            );

        var sortSelectors = new Dictionary<EmployeeSortBy, Expression<Func<Employee, object>>>
        {
            [EmployeeSortBy.CreatedDate] = e => e.CreatedDate,
            [EmployeeSortBy.ModifiedDate] = e => e.ModifiedDate,
            [EmployeeSortBy.Role] = e => e.Role,
            [EmployeeSortBy.FullName] = e => e.LastName + " " + e.Name + " " + e.Patronymic
        };

        query = ApplySorting(query, geq.SortBy, geq.IsSortDescending, sortSelectors);

        var totalCount = await query.CountAsync();

        query = ApplyPaging(query, geq);

        var employees = await query
            .AsNoTracking()
            .ToListAsync();

        return (employees, totalCount);
    }

    public async Task UpdateAsync(Employee employee)
    {
        _context.Employees.Update(employee);
        await _context.SaveChangesAsync();
    }
}
