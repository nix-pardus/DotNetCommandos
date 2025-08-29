using Domain.DTO.Order;

namespace Application.Interfaces
{
    public interface IOrderService
    {
        Task CreateAsync(OrderDto dto);
        Task<OrderDto> UpdateAsync(OrderDto dto);
        Task GetAsync(long id);
    }
}
