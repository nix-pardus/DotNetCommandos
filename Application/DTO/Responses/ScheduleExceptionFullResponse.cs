
using ServiceCenter.Domain.ValueObjects.Enums;

namespace ServiceCenter.Application.DTO.Responses;

/// <summary>
/// График работы
/// </summary>
/// <param name="Id">Идентификатор графика</param>
//TODO: заполнить
public record ScheduleExceptionFullResponse(
    Guid Id,
    ScheduleExceptionType? exceptionType,
    DateOnly EffectiveFrom,
    DateOnly EffectiveTo,
    Guid EmployeeId,
    bool IsDeleted
);
