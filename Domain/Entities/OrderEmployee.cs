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

    public Order Order { get; set; } = null!;
    public Employee Employee { get; set; } = null!;
}
