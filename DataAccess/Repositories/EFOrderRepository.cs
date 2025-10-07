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


}
