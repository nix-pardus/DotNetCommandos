using Riok.Mapperly.Abstractions;
using ServiceCenter.Application.DTO.Requests;
using ServiceCenter.Application.DTO.Responses;
using ServiceCenter.Domain.Entities;

namespace ServiceCenter.Application.Mappers;

[Mapper]
public static partial class ClientMapper
{
    public static partial Client ToEntity(ClientCreateRequest dto);
    public static partial Client ToEntity(ClientUpdateRequest dto);
    public static partial ClientFullResponse ToResponse(Client entity);
}