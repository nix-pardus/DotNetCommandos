
using ServiceCenter.Application.DTO.Requests;
using ServiceCenter.Application.DTO.Responses;
using ServiceCenter.Application.Interfaces;
using ServiceCenter.Application.Mappers;
using ServiceCenter.Domain.Entities;
using ServiceCenter.Domain.Interfaces;

namespace ServiceCenter.Application.Services;

/// <summary>
/// Сервис для работы с заказами
/// Реализует <see cref="IOrderService"/>
/// </summary>

public class OrderService : BaseService<Order, OrderCreateRequest, OrderUpdateRequest, OrderFullResponse, IOrderRepository>, IOrderService
{
    protected override OrderFullResponse ToDto(Order entity) => OrderMapper.ToResponse(entity);
    protected override Order ToEntity(OrderCreateRequest dto) => OrderMapper.ToEntity(dto);
    protected override Order ToEntity(OrderUpdateRequest dto) => OrderMapper.ToEntity(dto);

    public OrderService(IOrderRepository repository, ICurrentUserService currentUserService)
          : base(repository, currentUserService) 
    {
    }

    ///// <inheritdoc />
    public async Task<OrderFullResponse> GetAsync(Guid id)
    {
        var order = await _repository.GetByIdAsync(id);
        if (order == null) { return null; }
        return OrderMapper.ToResponse(order);
    }
}
