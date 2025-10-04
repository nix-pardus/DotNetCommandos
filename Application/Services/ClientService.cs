using ServiceCenter.Application.DTO.Requests;
using ServiceCenter.Application.DTO.Responses;
using ServiceCenter.Application.Interfaces;
using ServiceCenter.Application.Mappers;
using ServiceCenter.Domain.Interfaces;

namespace ServiceCenter.Application.Services;

/// <summary>
/// Сервис для работы с клиентами
/// Реализует <see cref="IClientService"/>
/// </summary>
public class ClientService(IClientRepository repository) : IClientService
{
    /// <inheritdoc />
    public async Task CreateAsync(ClientCreateRequest dto)
    {
        await repository.AddAsync(ClientMapper.ToEntity(dto));
    }

        public async Task DeleteAsync(Guid id)
        {
            await repository.DeleteAsync(id);
        }

    /// <inheritdoc />
    public Task<ClientFullResponse> UpdateAsync(ClientUpdateRequest dto)
    {
        throw new NotImplementedException();
    }
}
