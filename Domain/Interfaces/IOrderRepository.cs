using ServiceCenter.Domain.Entities;

namespace ServiceCenter.Domain.Interfaces;

/// <summary>
/// Репозиторий для работы с сущностью Заказ
/// </summary>
public interface IOrderRepository
{
    /// <summary>
    /// Добавить новый заказ
    /// </summary>
    /// <param name="order">Сущность заказа</param>
    Task AddAsync(Order order);

    /// <summary>
    /// Получить заказ по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор заказа</param>
    Task<Order> GetByIdAsync(Guid id);

    /// <summary>
    /// Получить все заказы
    /// </summary>
    Task<IEnumerable<Order>> GetAllAsync();

    /// <summary>
    /// Получить все заказы клиента
    /// </summary>
    /// <param name="clientId">Идентификатор клиента</param>
    Task<IEnumerable<Order>> GetByClientIdAsync(Guid clientId);

    /// <summary>
    /// Обновить существующий заказ
    /// </summary>
    /// <param name="order">Сущность заказа</param>
    Task UpdateAsync(Order order);
}
