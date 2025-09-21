namespace ServiceCenter.Domain.ValueObjects.Enums;

/// <summary>
/// Типы исключений из графика работы
/// </summary>
[Flags]
public enum ScheduleExceptionType
{
    /// <summary>
    /// Исключение без типа
    /// </summary>
    None = 0,

    /// <summary>
    /// Отпуск
    /// </summary>
    Vacation = 1,

    /// <summary>
    /// Больничный
    /// </summary>
    Sick = 2
}
