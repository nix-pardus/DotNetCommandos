using ServiceCenter.Application.DTO.Shared;
using ServiceCenter.Application.Mappers;
using ServiceCenter.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.Services
{
    public abstract class BaseService<TEntity, TResponse, TRepository>
    where TEntity : class, IEntity
    where TResponse : class
    where TRepository : IRepository<TEntity>
    {
        protected abstract TResponse ToDto(TEntity entity);
        protected abstract TEntity ToEntity(TResponse response);

        protected readonly TRepository _repository;
        public BaseService(TRepository repository)
        {
            _repository = repository;
        }
        public virtual async Task CreateAsync(TResponse dto)
        {
            await _repository.AddAsync(ToEntity(dto));
        }

        public virtual async Task DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }

        public virtual async Task<PagedResponse<TResponse>> GetByFiltersAsync(GetByFiltersRequest request)
        {
            var filterConditions = request.Filters?.Select(f =>
                (f.Field, f.Operator.ToString(), f.Value)) ?? Enumerable.Empty<(string, string, string)>();
            var (items, totalCount) = await _repository.GetByFiltersPagedAsync(
                filterConditions,
                request.LogicalOperator,
                request.PageNumber,
                request.PageSize
            );
            return new PagedResponse<TResponse>
            {
                Items = items.Select(ToDto).ToList(),
                TotalCount = totalCount,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };
        }

        protected virtual async Task<PagedResponse<TResponse>> GetByFiltersWithIncludesAsync(
            GetByFiltersRequest request,
            params Func<IQueryable<TEntity>, IQueryable<TEntity>>[] includes)
        {
            var filterConditions = request.Filters?.Select(f =>
                (f.Field, f.Operator.ToString(), f.Value)) ?? Enumerable.Empty<(string, string, string)>();

            var (items, totalCount) = await _repository.GetByFiltersPagedWithIncludesAsync(
                filterConditions,
                request.LogicalOperator,
                request.PageNumber,
                request.PageSize,
                includes);

            return new PagedResponse<TResponse>
            {
                Items = items.Select(ToDto).ToList(),
                TotalCount = totalCount,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };
        }

        public virtual async Task UpdateAsync(TResponse dto)
        {
            await _repository.UpdateAsync(ToEntity(dto));
        }
    }
}
