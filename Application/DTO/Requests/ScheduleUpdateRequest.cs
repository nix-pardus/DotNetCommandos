
namespace ServiceCenter.Application.DTO.Requests;

/// <summary>
/// График работы
/// </summary>
/// <param name="Id">Идентификатор графика</param>
//TODO: заполнить
public record ScheduleUpdateRequest(
    Guid Id,
    string Pattern,
    DateOnly EffectiveFrom,
    DateOnly EffectiveTo,
    TimeOnly StartTime,
    TimeOnly EndTime,
    Guid EmployeeId,
    bool IsDeleted
);
