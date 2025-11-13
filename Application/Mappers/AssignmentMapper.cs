using Riok.Mapperly.Abstractions;
using ServiceCenter.Application.DTO.Assignment;
using ServiceCenter.Domain.Entities;

namespace ServiceCenter.Application.Mappers;

[Mapper]
public static partial class AssignmentMapper
{
    public static partial AssignmentDto ToDto(OrderEmployee orderEmployee);
    public static partial OrderEmployee ToEntity(AssignmentDto dto);
}
