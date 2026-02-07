using UI.Models;
using UI.Models.Orders;

namespace UI.Services;

public interface IOrderService
{
    Task<PagingResponse<Order>> GetOrdersAsync(GetByFiltersRequest request);
    Task<Order> GetOrderByIdAsync(Guid id);
    Task CreateOrderAsync(OrderCreate order);
    Task<bool> UpdateOrderAsync(OrderUpdate order);
    Task<bool> DeleteOrderAsync(Guid id);
    Task<bool> UpdateOrderStatusAsync(Guid orderId, OrderStatus status);
}
