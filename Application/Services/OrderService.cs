
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

    public class OrderService : BaseService<Order,  OrderCreateRequest, OrderUpdateRequest,OrderFullResponse, IOrderRepository>, IOrderService
{
    protected override OrderFullResponse ToDto(Order entity) => OrderMapper.ToResponse(entity);
    protected override Order ToEntity(OrderCreateRequest dto) => OrderMapper.ToEntity(dto);
    protected override Order ToEntity(OrderUpdateRequest dto) => OrderMapper.ToEntity(dto);

    public OrderService(IOrderRepository repository)
          : base(repository) { }

    /// <inheritdoc />
    public override async Task CreateAsync(OrderCreateRequest create_dto)
    {
        var entity = new Order
        {  
            Id = Guid.NewGuid(),
            ClientId = create_dto.ClientId,
            EquipmentType = create_dto.EquipmentType,
            EquipmentModel = create_dto.EquipmentModel,
            IsWarranty = create_dto.IsWarranty,
            Problem = create_dto.Problem,
            Comment = create_dto.Comment,
            Priority = create_dto.Priority,
            Status = create_dto.Status,
            IsDeleted = false,
            StartDateTime = create_dto.StartDateTime,
            EndDateTime = create_dto.EndDateTime,
            CreatedDate = DateTime.UtcNow,
            CreatedById = Guid.Empty//ЗАПОЛНЯТЬ ПОСЛЕ РЕАЛИЗАЦИИ АВТОРИЗАЦИИ
        };

        await _repository.AddAsync(entity);
    }

    ///// <inheritdoc />
    public async Task<OrderFullResponse> GetAsync(Guid id)
    {
        var order = await _repository.GetByIdAsync(id);
        if (order == null) { return null; }
        return OrderMapper.ToResponse(order);
    }

    public override async Task<OrderFullResponse> UpdateAsync(OrderUpdateRequest dto)
    {
        await _repository.UpdateAsync(ToEntity(dto));
        var order = await _repository.GetByIdAsync(dto.Id);
        if (order == null) { return null; }
        return OrderMapper.ToResponse(order);
    }
}
