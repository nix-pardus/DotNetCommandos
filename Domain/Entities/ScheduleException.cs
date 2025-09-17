using ServiceCenter.Domain.ValueObjects.Enums;

namespace ServiceCenter.Domain.Entities;

/// <summary>
/// Сущность исключения из графика.
/// Наследуется от <see cref="EntityBase"/>, который содержит общие поля для всех сущностей (например, Id, CreatedDate, ModifiedDate).
/// </summary>
public class ScheduleException : EntityBase
{
    /// <summary>
    /// Тип исключения.
    /// </summary>
    public ScheduleExceptionType? ExceptionType { get; set; }

    /// <summary>
    /// Дата начала действия.
    /// </summary>
    public DateOnly EffectiveFrom { get; set; }

    /// <summary>
    /// Дата окончания действия.
    /// </summary>
    public DateOnly EffectiveTo { get; set; }
    public Employee Employee { get; set; }
    public Guid EmployeeId { get; set; }
}
