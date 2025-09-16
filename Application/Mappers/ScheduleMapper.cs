using Riok.Mapperly.Abstractions;
using ServiceCenter.Application.DTO.Schedule;
using ServiceCenter.Domain.Entities;

namespace ServiceCenter.Application.Mappers;

[Mapper]
public static partial class ScheduleMapper
{
    public static partial Schedule ToEntity(ScheduleDto dto);

    public static partial ScheduleDto ToDto(Schedule entity);
}