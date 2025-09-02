using Domain.DTO.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IClientService
    {
        //TODO: пока так, но в итоге надо сделать отденьные dto
        Task CreateAsync(ClientDto dto);
        Task<ClientDto> UpdateAsync(ClientDto dto);
        Task DeleteAsync(Guid id);

    }
}
