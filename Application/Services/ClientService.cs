using ServiceCenter.Application.DTO.Requests;
using ServiceCenter.Application.DTO.Responses;
using ServiceCenter.Application.Interfaces;
using ServiceCenter.Application.Mappers;
using ServiceCenter.Domain.Entities;
using ServiceCenter.Domain.Interfaces;

namespace ServiceCenter.Application.Services;

/// <summary>
/// Сервис для работы с клиентами
/// Реализует <see cref="IClientService"/>
/// </summary>
public class ClientService : BaseService<Client, ClientCreateRequest, ClientUpdateRequest, ClientFullResponse, IClientRepository>, IClientService
{
    protected override ClientFullResponse ToDto(Client entity) => ClientMapper.ToResponse(entity);
    protected override Client ToEntity(ClientCreateRequest dto) => ClientMapper.ToEntity(dto);
    protected override Client ToEntity(ClientUpdateRequest dto) => ClientMapper.ToEntity(dto);

    public ClientService(IClientRepository repository, ICurrentUserService currentUserService)
          : base(repository, currentUserService) { }
}
