using ServiceCenter.Domain.Entities;

namespace ServiceCenter.Domain.Interfaces;

/// <summary>
/// Репозиторий для работы с сущностью Заказ
/// </summary>
public interface IOrderRepository:IRepository<Order>
{
    /// <summary>
    /// Получить все заказы
    /// </summary>
    Task<IEnumerable<Order>> GetAllAsync();

    /// <summary>
    /// Получить все заказы клиента
    /// </summary>
    /// <param name="clientId">Идентификатор клиента</param>
    Task<IEnumerable<Order>> GetByClientIdAsync(Guid clientId);
}
