using ServiceCenter.Domain.DTO.Order;

namespace ServiceCenter.Application.Interfaces;

/// <summary>
/// Сервис для работы с заказами/заявками
/// </summary>
public interface IOrderService
{
    /// <summary>
    /// Создание нового заказа
    /// </summary>
    /// <param name="dto">DTO заказа</param>
    /// <returns>Задача выполнения операции</returns>
    Task CreateAsync(OrderDto dto);

    /// <summary>
    /// Обновление существующего заказа
    /// </summary>
    /// <param name="dto">DTO заказа с обновлёнными данными</param>
    /// <returns>Обновлённый заказ</returns>
    Task<OrderDto> UpdateAsync(OrderDto dto);

    /// <summary>
    /// Получение заказа по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор заказа</param>
    /// <returns>DTO заказа</returns>
    Task<OrderDto> GetAsync(Guid id);
}
