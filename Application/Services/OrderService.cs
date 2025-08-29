using Application.Interfaces;
using Domain.DTO.Order;
using Domain.Interfaces;

namespace Application.Services
{
    public class OrderService:IOrderService
    {
        public readonly IOrderRepository _repository;
        public OrderService(IOrderRepository repository)
        {
            _repository = repository;
        }
        public async Task CreateAsync(OrderDto dto)
        {
            await _repository.AddAsync(new Domain.Aggregates.Order(dto));
        }

        public Task<OrderDto> UpdateAsync(OrderDto dto)
        {
            throw new NotImplementedException();
        }

        public Task GetAsync(long id)
        {
            throw new NotImplementedException();
        }
    }
}
