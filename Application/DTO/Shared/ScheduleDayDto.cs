namespace ServiceCenter.Application.DTO.Shared;

/// <summary>
/// День графика
/// </summary>
//TODO: заполнить
public record ScheduleDayDto(
    string DayType,
    TimeOnly StartTime,
    TimeOnly EndTime,
    DateOnly Date
);
