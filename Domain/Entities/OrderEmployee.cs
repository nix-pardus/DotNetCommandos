namespace ServiceCenter.Domain.Entities;

/// <summary>
/// Сущность, представляющая связь между заказами и сотрудниками.
/// </summary>
public class OrderEmployee : EntityBase
{
    /// <summary>
    /// Идентификатор заказа.
    /// </summary>
    public Guid OrderId { get; set; }
    /// <summary>
    /// Идентификатор сотрудника.
    /// </summary>
    public Guid EmployeeId { get; set; }
    /// <summary>
    /// Признак, является ли сотрудник основным исполнителем заказа.
    /// </summary>
    public bool IsPrimary { get; set; }

    /// <summary>
    /// Сотрудник, создавший запись о назначении.
    /// </summary>
    public Guid CreatedById { get; set; }

    /// <summary>
    /// Сотрудник, последнего изменивший запись о назначении.
    /// </summary>
    public Guid? ModifiedById { get; set; }

    public Order Order { get; set; } = null!;
    public Employee Employee { get; set; } = null!;
}
