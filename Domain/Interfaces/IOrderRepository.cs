using ServiceCenter.Domain.Entities;

namespace ServiceCenter.Domain.Interfaces;

/// <summary>
/// Репозиторий для работы с сущностью Заказ
/// </summary>
public interface IOrderRepository:IRepository<Order>
{
}
