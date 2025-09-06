using Riok.Mapperly.Abstractions;
using ServiceCenter.Domain.DTO.Order;
using ServiceCenter.Domain.Entities;

namespace ServiceCenter.Application.Mappers;

[Mapper]
public static partial class OrderMapper
{
    public static partial Order ToEntity(OrderDto dto);

    public static partial OrderDto ToDto(Order entity);
}
