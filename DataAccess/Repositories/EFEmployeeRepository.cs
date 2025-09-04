using Microsoft.EntityFrameworkCore;
using ServiceCenter.Domain.Entities;
using ServiceCenter.Domain.Interfaces;

namespace ServiceCenter.Infrascructure.DataAccess.Repositories;

public class EFEmployeeRepository(DataContext context) : IEmployeeRepository
{
    private readonly DataContext _context = context;

    /// <summary>
    /// Добавить сотрудника
    /// </summary>
    /// <param name="employee">Объект добавляемого сотрудника</param>
    /// <returns></returns>
    public async Task AddAsync(Employee employee)
    {
        _context.Employees.Add(employee);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Удалить сотрудника
    /// </summary>
    /// <param name="id">Id удаляемого сотрудника</param>
    /// <returns></returns>
    public async Task DeleteAsync(Guid id)
    {
        var employee = await _context.Employees.SingleOrDefaultAsync(x => x.Id == id);
        if (employee != null)
        {
            employee.IsDeleted = true;
            _context.Employees.Update(employee!);
            await _context.SaveChangesAsync();
        }
        else
        {
            throw new ArgumentNullException("Employee is not exist.");
        }
    }

    /// <summary>
    /// Получить список всех актуальных сотрудников
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<Employee>> GetAllActiveAsync()
    {
        return await _context.Employees.Where(x => x.IsDeleted == false).ToArrayAsync();
    }

    /// <summary>
    /// Получить список всех сотрудников
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<Employee>> GetAllAsync()
    {
        return await _context.Employees.ToArrayAsync();
    }

    /// <summary>
    /// Получить сотрудника по Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<Employee> GetByIdAsync(Guid id)
    {
        return await _context.Employees.SingleOrDefaultAsync(x => x.Id == id);
    }

    /// <summary>
    /// Обновить сотрудника
    /// </summary>
    /// <param name="employee"></param>
    /// <returns></returns>
    public async Task UpdateAsync(Employee employee)
    {
        _context.Employees.Update(employee);
        await _context.SaveChangesAsync();
    }
}
