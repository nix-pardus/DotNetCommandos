using UI.Models;
using UI.Models.Clients;

namespace UI.Services;

public interface IClientService
{
    Task<PagingResponse<Client>> GetAllClientsAsync(GetByFiltersRequest request);
    Task<Client> GetClientByIdAsync(Guid id);
    Task CreateClientAsync(CreateClient client);
    Task<bool> UpdateClientAsync(ClientUpdate client);
    Task<bool> DeleteClientAsync(Guid id);
}