namespace ServiceCenter.Domain.DTO.Order;

/// <summary>
/// Заказ/заявка
/// </summary>
/// <param name="Id">Идентификатор заказа</param>
/// <param name="CreatedDate">Дата создания</param>
/// <param name="CreatedById">Идентификатор сотрудника, создавшего заказ</param>
/// <param name="ModifiedById">Дата последнего изменения</param>
/// <param name="ModifiedById">Идентификатор сотрудника, изменившего заказ</param>
/// <param name="ClientId">Идентификатор клиента</param>
/// <param name="EquipmentType">Тип оборудования</param>
/// <param name="EquipmentModel">Модель оборудования</param>
/// <param name="IsWarranty">Гарантийный/негарантийный</param>
/// <param name="Problem">Описание проблемы</param>
/// <param name="Note">Примечание</param>
/// <param name="Comment">Комментарий к заказу</param>
/// <param name="Lead">Источник лида</param>
/// <param name="Priority">Приоритет</param>
/// <param name="IsDeleted">Признак удаления</param>
/// <param name="StartDate">Начало интервала визита мастера</param>
/// <param name="EndDate">Окончание интервала визита мастера</param>
public record OrderDto(
    Guid Id,
    DateTime CreatedDate,
    Guid CreatedById,
    DateTime ModifiedDate,
    Guid ModifiedById,
    Guid ClientId,
    string EquipmentType,
    string? EquipmentModel,
    bool IsWarranty,
    string Problem,
    string? Note,
    string? Comment,
    string? Lead,
    int Priority,
    bool IsDeleted,
    DateTime StartDate,
    DateTime EndDate
);