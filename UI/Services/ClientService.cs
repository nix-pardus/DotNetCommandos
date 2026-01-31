using UI.Models;
using UI.Models.Clients;

namespace UI.Services;

public class ClientService : IClientService
{
    private readonly IApiService _apiService;

    public ClientService(IApiService apiService)
    {
        _apiService = apiService;
    }

    public async Task<PagingResponse<Client>> GetAllClientsAsync(GetByFiltersRequest request)
    {
        return await _apiService.GetAsync<PagingResponse<Client>>("api/Client/get-by-filters", request);
    }

    public Task<Client> GetClientByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task CreateClientAsync(CreateClient client)
    {
        await _apiService.PostAsync<CreateClient>("api/Client/create", client);
    }

    public async Task<bool> UpdateClientAsync(ClientUpdate client)
    {
        return await _apiService.PutAsync($"api/Client/update?id={client.Id}", client);
    }

    public async Task<bool> DeleteClientAsync(Guid id)
    {
        return await _apiService.DeleteAsync($"api/Client/delete?id={id}");
    }
}