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

    public Task<Order> GetOrderByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }


    public async Task<bool> UpdateOrderAsync(OrderUpdate order)
    {
        return await apiService.PutAsync($"api/Order/update?id={order.Id}", order);
    }
}
