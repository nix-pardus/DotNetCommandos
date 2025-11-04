using ServiceCenter.Domain.ValueObjects;

namespace ServiceCenter.Domain.Entities;

/// <summary>
/// Сущность заказа в сервисном центре.
/// Наследуется от <see cref="EntityBase"/>, который содержит общие поля для всех сущностей (например, Id, CreatedDate, ModifiedDate).
/// </summary>
public class Order : EntityBase
{
    /// <summary>
    /// Идентификатор сотрудника, создавшего заказ.
    /// </summary>
    public Guid CreatedById { get; set; }

    /// <summary>
    /// Идентификатор сотрудника, последнего изменившего заказ.
    /// </summary>
    public Guid? ModifiedById { get; set; }

    /// <summary>
    /// Идентификатор клиента, которому принадлежит заказ.
    /// </summary>
    public Guid ClientId { get; set; }

    /// <summary>
    /// Тип оборудования.
    /// </summary>
    public string? EquipmentType { get; set; }

    /// <summary>
    /// Модель оборудования (необязательное поле).
    /// </summary>
    public string? EquipmentModel { get; set; }

    /// <summary>
    /// Является ли заказ гарантийным.
    /// </summary>
    public bool IsWarranty { get; set; }

    /// <summary>
    /// Описание проблемы, с которой пришел клиент.
    /// </summary>
    public string Problem { get; set; } = null!;

    /// <summary>
    /// Комментарий сотрудника (необязательное поле).
    /// </summary>
    public string? Comment { get; set; }

    /// <summary>
    /// Приоритет заказа (например, 1 — высокий, 5 — низкий).
    /// </summary>
    public int Priority { get; set; }

    /// <summary>
    /// Статус заказа.
    /// </summary>
    public OrderStatus Status { get; set; }

    /// <summary>
    /// Дата и время начала выполнения заказа.
    /// </summary>
    public DateTime? StartDateTime { get; set; }

    /// <summary>
    /// Дата и время окончания выполнения заказа.
    /// </summary>
    public DateTime? EndDateTime { get; set; }

    /// <summary>
    /// Назначенные сотрудники на заказ
    /// </summary>
    public ICollection<OrderEmployee> AssignedEmployees { get; set; } = new List<OrderEmployee>();

    /// <summary>
    /// Главный сотрудник заказа (вычисляемое свойство)
    /// </summary>
    public Employee? LeadEmployee => AssignedEmployees.FirstOrDefault(x => x.IsPrimary)?.Employee;
}
