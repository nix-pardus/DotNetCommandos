using Microsoft.EntityFrameworkCore;
using ServiceCenter.Domain.Entities;
using ServiceCenter.Domain.Interfaces;

namespace ServiceCenter.Infrascructure.DataAccess.Repositories;

public class EFClientRepository(DataContext context) : IClientRepository
{
    private readonly DataContext _context = context;

    public async Task AddAsync(Client client)
    {
        _context.Clients.Add(client);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var client = await _context.Clients.SingleOrDefaultAsync(x=>x.Id == id);
        //TODO: Обрабатывать  null
        _context.Clients.Remove(client);
        await _context.SaveChangesAsync();
    }
    //TODO: будем реалистами - нужна пагинация
    public async Task<IEnumerable<Client>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<Client> GetByIdAsync(Guid id)
    {
        return await _context.Clients.SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task UpdateAsync(Client client)
    {
        _context.Clients.Update(client);
        await _context.SaveChangesAsync();
    }
}
