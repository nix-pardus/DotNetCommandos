using ServiceCenter.Application.DTO.Client;
using ServiceCenter.Application.DTO.Shared;
using ServiceCenter.Application.Interfaces;
using ServiceCenter.Application.Mappers;
using ServiceCenter.Domain.DTO.Order;
using ServiceCenter.Domain.Entities;
using ServiceCenter.Domain.Interfaces;
using System.Xml.Linq;

namespace ServiceCenter.Application.Services;

/// <summary>
/// Сервис для работы с заказами
/// Реализует <see cref="IOrderService"/>
/// </summary>

    public class OrderService : BaseService<Order, OrderDto, IOrderRepository>, IOrderService
{
    protected override OrderDto ToDto(Order entity) => OrderMapper.ToDto(entity);
    protected override Order ToEntity(OrderDto dto) => OrderMapper.ToEntity(dto);

    public OrderService(IOrderRepository repository)
          : base(repository) { }

    /// <inheritdoc />
    public async Task CreateAsync(CreateOrderDto create_dto)
    {
        var dto = new OrderDto
        (
             Id: Guid.NewGuid(),
             CreatedDate: DateTime.UtcNow,
             CreatedById: Guid.Empty,
             ModifiedDate: null,
             ModifiedById: null,
             ClientId: create_dto.ClientId,
             EquipmentType: create_dto.EquipmentType,
             EquipmentModel: create_dto.EquipmentModel,
             IsWarranty: create_dto.IsWarranty,
             Problem: create_dto.Problem,
             Note: create_dto.Note,
             Comment: create_dto.Comment,
             Lead: create_dto.Lead,
             Priority: create_dto.Priority,
             IsDeleted: false,
             StartDate: create_dto.StartDate,
             EndDate: create_dto.EndDate
        );
        await _repository.AddAsync(OrderMapper.ToEntity(dto));
    }

    ///// <inheritdoc />
    //public Task<OrderDto> UpdateAsync(OrderDto dto)
    //{
    //    throw new NotImplementedException();
    //}

    //public async Task DeleteAsync(Guid id)
    //{
    //    await repository.DeleteAsync(id);
    //}

    ///// <inheritdoc />
    public async Task<OrderDto> GetAsync(Guid id)
    {
        var order = await _repository.GetByIdAsync(id);
        if (order == null) { return null; }
        return OrderMapper.ToDto(order);
    }

    public async Task<IEnumerable<OrderDto>> GetAllAsync()
    {
        var orders = await _repository.GetAllAsync();
        return orders.Select(OrderMapper.ToDto);

    }

    public async Task<IEnumerable<OrderDto>> GetByClientIdAsync(Guid ClientId)
    {
        var orders = await _repository.GetByClientIdAsync(ClientId);
        return orders.Select(OrderMapper.ToDto);
    }

    ///// <summary>
    ///// Получение списка заказов c фильтрацией/пагинацией
    ///// </summary>   
    ///// <returns>список DTO заказов</returns>
    //public async Task<PagedResponse<ClientDto>> GetByFiltersAsync(GetByFiltersRequest request)
    //{}
}
