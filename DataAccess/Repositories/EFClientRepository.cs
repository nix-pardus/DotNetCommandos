using Domain.Aggregates;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrascructure.DataAccess.Repositories
{
    public class EFClientRepository : IClientRepository
    {
        private readonly DataContext _context;
        public EFClientRepository(DataContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Client client)
        {
            _context.Clients.Add(client);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var client = await _context.Clients.SingleOrDefaultAsync(x=>x.Id == id);
            if (client != null)
            {
                client.IsDeleted = true;
            }
            else
            {
                throw new ArgumentNullException(@$"Client ""{id}"" is not exists");
            }
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
}
