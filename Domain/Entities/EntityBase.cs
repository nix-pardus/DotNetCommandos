using ServiceCenter.Domain.Interfaces;

namespace ServiceCenter.Domain.Entities;

/// <summary>
/// Базовая сущность с Id, датой создания и логическим удалением
/// Используется для всех сущностей домена
/// </summary>
public abstract class EntityBase:IEntity
{
    /// <summary>Идентификатор сущности</summary>
    public Guid Id { get; set; }

    /// <summary>Дата создания сущности</summary>
    public DateTime CreatedDate { get; set; }

    /// <summary>Дата последнего изменения</summary>
    public DateTime? ModifiedDate { get; set; }

    /// <summary>Признак логического удаления</summary>
    public bool IsDeleted { get; set; }
}
