using Riok.Mapperly.Abstractions;
using ServiceCenter.Application.DTO.Requests;
using ServiceCenter.Application.DTO.Responses;
using ServiceCenter.Domain.Entities;

namespace ServiceCenter.Application.Mappers;

[Mapper]
public static partial class ScheduleExceptionMapper
{
    public static partial ScheduleException ToEntity(ScheduleExceptionCreateRequest dto);
    public static partial ScheduleException ToEntity(ScheduleExceptionUpdateRequest dto);

    public static partial ScheduleExceptionFullResponse ToResponse(ScheduleException entity);
}