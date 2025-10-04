
using ServiceCenter.Domain.ValueObjects.Enums;

namespace ServiceCenter.Application.DTO.Requests;

/// <summary>
/// График работы
/// </summary>
/// <param name="Id">Идентификатор графика</param>
//TODO: заполнить
public record ScheduleExceptionUpdateRequest(
    Guid Id,
    ScheduleExceptionType? exceptionType,
    DateOnly EffectiveFrom,
    DateOnly EffectiveTo,
    Guid EmployeeId,
    bool IsDeleted
);
