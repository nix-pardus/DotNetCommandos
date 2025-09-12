using Microsoft.EntityFrameworkCore;
using ServiceCenter.Domain.Entities;
using ServiceCenter.Domain.Interfaces;
using ServiceCenter.Domain.Queries;
using ServiceCenter.Domain.ValueObjects.Enums;

namespace ServiceCenter.Infrascructure.DataAccess.Repositories.Employees;

public class EFEmployeeRepository(DataContext context) : IEmployeeRepository
{
    private readonly DataContext _context = context;

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
        if (geq.PageNumber < 1) geq.PageNumber = 1;
        if (geq.PageSize < 1) geq.PageSize = 10;

        var query = _context.Employees.AsQueryable();

        //TODO: желательно сделать рефакторинг для переиспользования кода фильтрации, сортировки, пагинации
        #region Применяем фильтры, если они есть
        if (!string.IsNullOrWhiteSpace(geq.Name))
            query = query.Where(x => x.Name.Contains(geq.Name));

        if (!string.IsNullOrWhiteSpace(geq.LastName))
            query = query.Where(x => x.LastName.Contains(geq.LastName));

        if (!string.IsNullOrWhiteSpace(geq.Patronymic))
            query = query.Where(x => x.Patronymic.Contains(geq.Patronymic));

        if (!string.IsNullOrWhiteSpace(geq.Address))
            query = query.Where(x => x.Address.Contains(geq.Address));

        if (!string.IsNullOrWhiteSpace(geq.Email))
            query = query.Where(x => x.Email.Contains(geq.Email));

        if (!string.IsNullOrWhiteSpace(geq.PhoneNumber))
            query = query.Where(x => x.PhoneNumber.Contains(geq.PhoneNumber));

        if (geq.Role.HasValue)
            query = query.Where(x => x.Role == geq.Role.Value);

        if (geq.CreatedById.HasValue)
            query = query.Where(x => x.CreatedById == geq.CreatedById.Value);

        if (geq.CreatedDateAfter.HasValue)
            query = query.Where(x => x.CreatedDate >= geq.CreatedDateAfter.Value);

        if (geq.CreatedDateBefore.HasValue)
            query = query.Where(x => x.CreatedDate <= geq.CreatedDateBefore.Value);

        if (geq.IsDeleted.HasValue)
            query = query.Where(x => x.IsDeleted == geq.IsDeleted);
        #endregion

        query = EmployeeSortHelper.ApplySorting(query, geq.SortBy, geq.IsSortDescending);

        var totalCount = await query.CountAsync();

        var employees = await query
            .Skip((geq.PageNumber - 1) * geq.PageSize)
            .Take(geq.PageSize)
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
