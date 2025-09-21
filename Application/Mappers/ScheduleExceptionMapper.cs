using Riok.Mapperly.Abstractions;
using ServiceCenter.Application.DTO.Schedule;
using ServiceCenter.Domain.Entities;

namespace ServiceCenter.Application.Mappers;

[Mapper]
public static partial class ScheduleExceptionMapper
{
    public static partial ScheduleException ToEntity(ScheduleExceptionDto dto);

    public static partial ScheduleExceptionDto ToDto(ScheduleException entity);
}