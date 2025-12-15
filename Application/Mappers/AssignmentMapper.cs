using Riok.Mapperly.Abstractions;
using ServiceCenter.Application.DTO.Requests;
using ServiceCenter.Application.DTO.Responses;
using ServiceCenter.Domain.Entities;

namespace ServiceCenter.Application.Mappers;

[Mapper]
public static partial class AssignmentMapper
{
    public static partial OrderEmployee ToEntity(AssignmentResponse response);
    public static partial OrderEmployee ToEntity(AssignmentCreateRequest request);
    public static partial OrderEmployee ToEntity(AssignmentUpdateRequest request);
    public static partial AssignmentResponse ToResponse(OrderEmployee entity);
}
