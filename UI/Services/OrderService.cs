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

    public Task<bool> DeleteOrderAsync(Guid id)
    {
        throw new NotImplementedException();
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
