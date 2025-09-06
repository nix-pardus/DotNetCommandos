using ServiceCenter.Application.DTO.Client;
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
    public async Task CreateAsync(ClientDto dto)
    {
        await repository.AddAsync(ClientMapper.ToEntity(dto));
    }

    /// <inheritdoc />
    public Task<ClientDto> DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task<ClientDto> UpdateAsync(ClientDto dto)
    {
        throw new NotImplementedException();
    }
}
