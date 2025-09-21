
namespace ServiceCenter.Application.DTO.Schedule;

/// <summary>
/// График работы
/// </summary>
/// <param name="Id">Идентификатор графика</param>
//TODO: заполнить
public record ScheduleDto(
    string Pattern,
    DateOnly EffectiveFrom,
    DateOnly EffectiveTo,
    TimeOnly StartTime,
    TimeOnly EndTime,
    Guid EmployeeId,
    bool IsDeleted
);
