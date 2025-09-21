using Microsoft.EntityFrameworkCore;
using ServiceCenter.Domain.Entities;
using ServiceCenter.Domain.Interfaces;


namespace ServiceCenter.Infrascructure.DataAccess.Repositories;

public class EFOrderRepository(DataContext context) : IOrderRepository
{
    private readonly DataContext _context = context;

    /// <summary>
    /// Создание нового заказа
    /// </summary>
    public async Task AddAsync(Order order)
    {
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();
    }
    /// <summary>
    /// Чтение заказа по идентификатору
    /// </summary>
    public async Task<Order> GetByIdAsync(Guid id)
    {
        return await _context.Orders.FindAsync(id);
    }
    /// <summary>
    /// Чтение всех заказов
    /// </summary>
    public async Task<IEnumerable<Order>> GetAllAsync()
    {
        return await _context.Orders.ToListAsync();

    }
    /// <summary>
    /// Чтение всех заказов клиента
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<Order>> GetByClientIdAsync(Guid ClientId)
    {
        return await _context.Orders.Where(x => x.ClientId == ClientId).ToListAsync();
    }

    /// <summary>
    /// Редактирование Заказа
    /// </summary>
    /// <param name="order">новое состояние заказа</param>
    /// <returns></returns>
    public async Task UpdateAsync(Order order)
    {
        _context.Orders.Update(order);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var order = await _context.Orders.SingleOrDefaultAsync(t => t.Id == id);
        if (order != null)
        {
            //реализовано не физическое удаление из БД, а проставление соответствующего признака
            order.IsDeleted = true;
            //_context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }
    }
}
