using Riok.Mapperly.Abstractions;
using ServiceCenter.Application.DTO.Requests;
using ServiceCenter.Application.DTO.Responses;
using ServiceCenter.Domain.Entities;

namespace ServiceCenter.Application.Mappers;

[Mapper]
public static partial class OrderMapper
{
    public static partial Order ToEntity(OrderCreateRequest dto);
    public static partial Order ToEntity(OrderUpdateRequest dto);
    public static partial OrderFullResponse ToResponse(Order entity);
}
