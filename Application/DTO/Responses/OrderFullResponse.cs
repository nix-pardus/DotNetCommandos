using ServiceCenter.Domain.ValueObjects.Enums;

namespace ServiceCenter.Application.DTO.Responses;

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
/// <param name="Comment">Комментарий к заказу</param>
/// <param name="Priority">Приоритет</param>
/// <param name="IsDeleted">Признак удаления</param>
/// <param name="StartDateTime">Начало интервала визита мастера</param>
/// <param name="EndDateTime">Окончание интервала визита мастера</param>
public record OrderFullResponse(
    Guid Id,
    Guid CreatedById,
    Guid? ModifiedById,
    Guid ClientId, 
    string? EquipmentType,
    string? EquipmentModel,
    bool IsWarranty,
    string Problem,
    string? Comment,
    int Priority,
    DateTime CreatedDate,
    DateTime? ModifiedDate,
    bool IsDeleted,
    DateTime? EndDateTime,
    DateTime? StartDateTime,
    OrderStatus Status
); 


