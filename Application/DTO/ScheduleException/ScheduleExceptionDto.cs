
using ServiceCenter.Domain.ValueObjects.Enums;

namespace ServiceCenter.Application.DTO.Schedule;

/// <summary>
/// График работы
/// </summary>
/// <param name="Id">Идентификатор графика</param>
//TODO: заполнить
public record ScheduleExceptionDto(
    ScheduleExceptionType? exceptionType,
    DateOnly EffectiveFrom,
    DateOnly EffectiveTo,
    Guid EmployeeId,
    bool IsDeleted
);
