using Microsoft.EntityFrameworkCore;
using ServiceCenter.Domain.Entities;
using ServiceCenter.Domain.Interfaces;

namespace ServiceCenter.Infrascructure.DataAccess.Repositories;

public class EFClientRepository : BaseRepository<Client>, IClientRepository
{
    private readonly DataContext _context;
    private readonly IFilterBuilder<Client> _filterBuilder;
    public EFClientRepository(DataContext context, IFilterBuilder<Client> filterBuilder)
        :base(context, filterBuilder) { }
}
