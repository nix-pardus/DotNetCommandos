using UI.Models;
using UI.Models.Orders;

namespace UI.Services;

public class OrderService(IApiService apiService) : IOrderService
{
    public async Task<PagingResponse<Order>> GetOrdersAsync(GetByFiltersRequest request)
    {
        return await apiService.GetAsync<PagingResponse<Order>>("api/Order/getByFilters", request);
    }
    public async Task CreateOrderAsync(OrderCreate order)
    {
        await apiService.PostAsync("api/Order/create", order);
    }

    public async Task<bool> DeleteOrderAsync(Guid id)
    {
        return await apiService.DeleteAsync($"api/Order/delete?orderId={id}");
    }

    public async Task<Order> GetOrderByIdAsync(Guid id)
    {
        return await apiService.GetAsync<Order>($"api/Order/getById", id);
    }


    public async Task<bool> UpdateOrderAsync(OrderUpdate order)
    {
        return await apiService.PutAsync($"api/Order/update?id={order.Id}", order);
    }

    public async Task<bool> UpdateOrderStatusAsync(Guid orderId, OrderStatus status)
    {
        try
        {
            var order = await GetOrderByIdAsync(orderId);

            var orderUpdate = new OrderUpdate
            {
                ClientId = order.ClientId,
                Comment = order.Comment,
                Status = status,
                EndDateTime = order.EndDateTime,
                EquipmentModel = order.EquipmentModel,
                EquipmentType = order.EquipmentType,
                Id = order.Id,
                IsWarranty = order.IsWarranty,
                Priority = order.Priority,
                Problem = order.Problem,
                StartDateTime = order.StartDateTime
            };

            return await UpdateOrderAsync(orderUpdate);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка обновления статуса заявки: {ex.Message}");
            return false;
        }
    }
}
