
using Microsoft.EntityFrameworkCore;
using ServiceCenter.Application.DTO.Requests;
using ServiceCenter.Application.DTO.Responses;
using ServiceCenter.Application.DTO.Shared;
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
    protected override OrderFullResponse ToDto(Order entity) => OrderMapper.ToResponseWithClient(entity);
    protected override Order ToEntity(OrderCreateRequest dto) => OrderMapper.ToEntity(dto);
    protected override Order ToEntity(OrderUpdateRequest dto) => OrderMapper.ToEntity(dto);

    public OrderService(IOrderRepository repository, ICurrentUserService currentUserService)
          : base(repository, currentUserService)
    {
    }

    public async override Task<PagedResponse<OrderFullResponse>> GetByFiltersAsync(GetByFiltersRequest request)
    {
        var filterConditions = request.Filters?.Select(f =>
            (f.Field, f.Operator.ToString(), f.Value)) ?? Enumerable.Empty<(string, string, string)>();

        var sortDefinitions = request.SortBy?.Select(s => (s.Field, s.Direction)) ?? Enumerable.Empty<(string, bool)>();

        var (items, totalCount) = await _repository.GetByFiltersPagedWithIncludesAsync(
            filterConditions,
            request.LogicalOperator,
            request.PageNumber,
            request.PageSize,
            sortDefinitions,
            q => q.Include(o => o.Client)
        );

        return new PagedResponse<OrderFullResponse>
        {
            Items = items.Select(ToDto).ToList(),
            TotalCount = totalCount,
            PageNumber = request.PageNumber,
            PageSize = request.PageSize
        };
    }

    ///// <inheritdoc />
    public async Task<OrderFullResponse> GetAsync(Guid id)
    {
        var filter = new List<(string Field, string Operator, string Value)> { ("Id", "Equals", id.ToString()) };
        var (items, _) = await _repository.GetByFiltersPagedWithIncludesAsync(filter, "AND", 1, 1, null, q => q.Include(o => o.Client));
        var order = items.FirstOrDefault();
        return order != null ? ToDto(order) : null!;
    }
}
