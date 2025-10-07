using ServiceCenter.Application.DTO.Shared;
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
    Task CreateAsync(CreateOrderDto dto);

    /// <summary>
    /// Обновление существующего заказа
    /// </summary>
    /// <param name="dto">DTO заказа с обновлёнными данными</param>
    /// <returns>Обновлённый заказ</returns>
    Task UpdateAsync(OrderDto dto);

    /// <summary>
    /// Получение заказа по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор заказа</param>
    /// <returns>DTO заказа</returns>
    Task<OrderDto> GetAsync(Guid id);


    /// <summary>
    /// Получение списка заказов c фильтрацией/пагинацией
    /// </summary>   
    /// <returns>список DTO заказов</returns>
    Task<PagedResponse<OrderDto>> GetByFiltersAsync(GetByFiltersRequest request);

    /// <summary>
    /// Удаление заказа по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор заказа</param>
    /// <returns>DTO заказа</returns>
    Task DeleteAsync(Guid id);
}
