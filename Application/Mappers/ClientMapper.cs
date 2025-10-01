using Riok.Mapperly.Abstractions;
using ServiceCenter.Application.DTO.Client;
using ServiceCenter.Application.Interfaces;
using ServiceCenter.Domain.Entities;

namespace ServiceCenter.Application.Mappers;

[Mapper]
public static partial class ClientMapper
{
    public static partial Client ToEntity(ClientDto dto);

    public static partial ClientDto ToDto(Client entity);
}