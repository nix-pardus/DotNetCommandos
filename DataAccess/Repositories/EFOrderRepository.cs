using Microsoft.EntityFrameworkCore;
using ServiceCenter.Domain.Entities;
using ServiceCenter.Domain.Interfaces;


namespace ServiceCenter.Infrascructure.DataAccess.Repositories;

public class EFOrderRepository: BaseRepository<Order>, IOrderRepository
{
    private readonly DataContext _context;
    private readonly IFilterBuilder<Order> _filterBuilder;
    public EFOrderRepository(DataContext context, IFilterBuilder<Order> filterBuilder)
        : base(context, filterBuilder) { }

   
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
}
