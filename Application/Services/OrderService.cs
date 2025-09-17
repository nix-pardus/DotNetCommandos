using ServiceCenter.Application.Interfaces;
using ServiceCenter.Application.Mappers;
using ServiceCenter.Domain.DTO.Order;
using ServiceCenter.Domain.Interfaces;

namespace ServiceCenter.Application.Services;

/// <summary>
/// Сервис для работы с заказами
/// Реализует <see cref="IOrderService"/>
/// </summary>
public class OrderService(IOrderRepository repository) : IOrderService
{
    /// <inheritdoc />
    public async Task CreateAsync(OrderDto dto)
    {
        await repository.AddAsync(OrderMapper.ToEntity(dto));
    }

    /// <inheritdoc />
    public Task<OrderDto> UpdateAsync(OrderDto dto)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(Guid id)
    {
        await repository.DeleteAsync(id);
    }

    /// <inheritdoc />
    public Task<OrderDto> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}
