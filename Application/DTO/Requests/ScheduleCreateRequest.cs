namespace ServiceCenter.Application.DTO.Requests;

/// <summary>
/// График работы
/// </summary>
/// <param name="Id">Идентификатор графика</param>
//TODO: заполнить
public record ScheduleCreateRequest(
    string Pattern,
    DateOnly EffectiveFrom,
    DateOnly EffectiveTo,
    TimeOnly StartTime,
    TimeOnly EndTime,
    Guid EmployeeId
);
