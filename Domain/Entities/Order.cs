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
    public Guid ModifiedById { get; set; }

    /// <summary>
    /// Идентификатор клиента, которому принадлежит заказ.
    /// </summary>
    public Guid ClientId { get; set; }

    /// <summary>
    /// Тип оборудования.
    /// </summary>
    public string EquipmentType { get; set; } = null!;

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
    /// Дополнительные заметки по заказу (необязательное поле).
    /// </summary>
    public string? Note { get; set; }

    /// <summary>
    /// Комментарий сотрудника (необязательное поле).
    /// </summary>
    public string? Comment { get; set; }

    /// <summary>
    /// Лид/менеджер, ответственный за заказ (необязательное поле).
    /// </summary>
    public string? Lead { get; set; }

    /// <summary>
    /// Приоритет заказа (например, 1 — высокий, 5 — низкий).
    /// </summary>
    public int Priority { get; set; }

    /// <summary>
    /// Дата начала выполнения заказа.
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    /// Дата окончания выполнения заказа.
    /// </summary>
    public DateTime EndDate { get; set; }
}
