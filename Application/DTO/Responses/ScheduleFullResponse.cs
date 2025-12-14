
namespace ServiceCenter.Application.DTO.Responses;

/// <summary>
/// График работы
/// </summary>
/// <param name="Id">Идентификатор графика</param>
//TODO: заполнить
public record ScheduleFullResponse(
    Guid Id,
    string Pattern,
    DateOnly EffectiveFrom,
    DateOnly EffectiveTo,
    TimeOnly StartTime,
    TimeOnly EndTime,
    Guid EmployeeId,
    bool IsDeleted
);
