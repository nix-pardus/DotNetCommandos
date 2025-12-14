using Riok.Mapperly.Abstractions;
using ServiceCenter.Application.DTO.Requests;
using ServiceCenter.Application.DTO.Responses;
using ServiceCenter.Domain.Entities;

namespace ServiceCenter.Application.Mappers;

[Mapper]
public static partial class ScheduleMapper
{
    public static partial Schedule ToEntity(ScheduleCreateRequest dto);
    public static partial Schedule ToEntity(ScheduleUpdateRequest dto);
    public static partial ScheduleFullResponse ToResponse(Schedule entity);
}