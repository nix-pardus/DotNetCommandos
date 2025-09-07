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

        public async Task DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }

    /// <inheritdoc />
    public Task<ClientDto> UpdateAsync(ClientDto dto)
    {
        throw new NotImplementedException();
    }
}
