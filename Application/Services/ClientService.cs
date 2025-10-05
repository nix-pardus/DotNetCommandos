using ServiceCenter.Application.DTO.Client;
using ServiceCenter.Application.DTO.Shared;
using ServiceCenter.Application.Interfaces;
using ServiceCenter.Application.Mappers;
using ServiceCenter.Domain.Entities;
using ServiceCenter.Domain.Interfaces;

namespace ServiceCenter.Application.Services;

/// <summary>
/// Сервис для работы с клиентами
/// Реализует <see cref="IClientService"/>
/// </summary>
public class ClientService : BaseService<Client, ClientDto, IClientRepository>, IClientService
{
    protected override ClientDto ToDto(Client entity) => ClientMapper.ToDto(entity);
    protected override Client ToEntity(ClientDto dto) => ClientMapper.ToEntity(dto);

    public ClientService(IClientRepository repository)
          : base(repository) { }
    
}
