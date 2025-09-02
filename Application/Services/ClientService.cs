using Application.Interfaces;
using Domain.DTO.Client;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ClientService : IClientService
    {
        public readonly IClientRepository _repository;
        public ClientService(IClientRepository repository) 
        {  
            _repository = repository; 
        }
        public async Task CreateAsync(ClientDto dto)
        {
            var client = new Domain.Aggregates.Client(dto);
            await _repository.AddAsync(client);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }

        public Task<ClientDto> UpdateAsync(ClientDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
