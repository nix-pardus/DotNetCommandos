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
    public abstract class BaseService<TEntity, TCreateRequest, TUpdateRequest, TResponse, TRepository>
    where TEntity : class, IEntity
    where TCreateRequest : class
    where TUpdateRequest : class
    where TResponse : class
    where TRepository : IRepository<TEntity>
    {
        protected abstract TResponse ToDto(TEntity entity);
        protected abstract TEntity ToEntity(TCreateRequest request);
        protected abstract TEntity ToEntity(TUpdateRequest request);

        protected readonly TRepository _repository;
        public BaseService(TRepository repository)
        {
            _repository = repository;
        }
        public virtual async Task CreateAsync(TCreateRequest dto)
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

        public virtual Task<TResponse> UpdateAsync(TUpdateRequest dto)
        {
            throw new NotImplementedException();
        }
    }
}
