namespace ServiceCenter.Domain.Entities;

/// <summary>
/// Сущность графика работы сотрудника.
/// Наследуется от <see cref="EntityBase"/>, который содержит общие поля для всех сущностей (например, Id, CreatedDate, ModifiedDate).
/// </summary>
public class Schedule : EntityBase
{
    /// <summary>
    /// Схема работы.
    /// </summary>
    public string Pattern { get; set; } = null!;

    /// <summary>
    /// Дата начала действия.
    /// </summary>
    public DateOnly EffectiveFrom { get; set; }

    /// <summary>
    /// Дата окончания действия.
    /// </summary>
    public DateOnly? EffectiveTo { get; set; }

    /// <summary>
    /// Время начала работы.
    /// </summary>
    public TimeOnly StartTime { get; set; }

    /// <summary>
    /// Время окончания работы.
    /// </summary>
    public TimeOnly EndTime { get; set; }
    public Employee Employee { get; set; }
    public Guid EmployeeId { get; set; }

    /// <summary>
    /// Сотрудник, создавший запись о графике.
    /// </summary>
    public Guid CreatedById { get; set; }

    /// <summary>
    /// Сотрудник, последнего изменивший запись о графике.
    /// </summary>
    public Guid? ModifiedById { get; set; }

}
