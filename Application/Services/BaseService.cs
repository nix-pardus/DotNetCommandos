using ServiceCenter.Application.DTO.Shared;
using ServiceCenter.Application.Interfaces;
using ServiceCenter.Application.Mappers;
using ServiceCenter.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
        protected readonly ICurrentUserService? _currentUserService;
        public BaseService(TRepository repository, ICurrentUserService? currentUserService = null)
        {
            _repository = repository;
            _currentUserService = currentUserService;
        }
        public virtual async Task CreateAsync(TCreateRequest dto)
        {
            var entity = ToEntity(dto);

            entity.CreatedDate = DateTime.UtcNow;

            if(_currentUserService?.UserId is Guid userId)
            {
                var prop = entity.GetType().GetProperty("CreatedById", BindingFlags.Public | BindingFlags.Instance);
                if (prop != null && prop.CanWrite && prop.PropertyType == typeof(Guid))
                {
                    prop.SetValue(entity, userId);
                }
            }

            await _repository.AddAsync(entity);
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

        public virtual async Task UpdateAsync(TUpdateRequest dto)
        {
            var entity = ToEntity(dto);

            var id = entity.Id;
            if(id == Guid.Empty)
            {
                var idPropDto = typeof(TUpdateRequest).GetProperty("Id", BindingFlags.Public | BindingFlags.Instance);
                if(idPropDto != null && idPropDto.PropertyType == typeof(Guid))
                {
                    id = (Guid)(idPropDto.GetValue(dto) ?? Guid.Empty);
                }
            }

            if(id == Guid.Empty)
            {
                throw new ArgumentException("Update DTO does not contain Id.");
            }

            var existing = await _repository.GetByIdAsync(id);
            if (existing == null)
            {
                throw new InvalidOperationException($"Entity with id {id} not found.");
            }

            var excluded = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                "CreatedDate", "CreatedById"
            };

            var props = typeof(TEntity).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.CanWrite && !excluded.Contains(p.Name));

            foreach (var prop in props)
            {
                var newValue = prop.GetValue(entity);
                prop.SetValue(existing, newValue);
            }

            existing.ModifiedDate = DateTime.UtcNow;

            if(_currentUserService?.UserId is Guid userId)
            {
                var prop = existing.GetType().GetProperty("ModifiedById", BindingFlags.Public | BindingFlags.Instance);
                if (prop != null && prop.CanWrite && (prop.PropertyType == typeof(Guid) || prop.PropertyType == typeof(Guid?)))
                {
                    prop.SetValue(existing, userId);
                }
            }

            await _repository.UpdateAsync(existing);
        }
    }
    //TODO: Закомментировал и вроде все работает. Удалить после проверки.
    // public abstract class BaseService<TEntity, TResponse, TRepository>
    //where TEntity : class, IEntity
    //where TResponse : class
    //where TRepository : IRepository<TEntity>
    // {
    //     protected abstract TResponse ToDto(TEntity entity);
    //     protected abstract TEntity ToEntity(TResponse response);

    //     protected readonly TRepository _repository;
    //     public BaseService(TRepository repository)
    //     {
    //         _repository = repository;
    //     }
    //     public virtual async Task CreateAsync(TResponse dto)
    //     {
    //         await _repository.AddAsync(ToEntity(dto));
    //     }

    //     public virtual async Task DeleteAsync(Guid id)
    //     {
    //         await _repository.DeleteAsync(id);
    //     }

    //     public virtual async Task<PagedResponse<TResponse>> GetByFiltersAsync(GetByFiltersRequest request)
    //     {
    //         var filterConditions = request.Filters?.Select(f =>
    //             (f.Field, f.Operator.ToString(), f.Value)) ?? Enumerable.Empty<(string, string, string)>();
    //         var (items, totalCount) = await _repository.GetByFiltersPagedAsync(
    //             filterConditions,
    //             request.LogicalOperator,
    //             request.PageNumber,
    //             request.PageSize
    //         );
    //         return new PagedResponse<TResponse>
    //         {
    //             Items = items.Select(ToDto).ToList(),
    //             TotalCount = totalCount,
    //             PageNumber = request.PageNumber,
    //             PageSize = request.PageSize
    //         };
    //     }

    //     protected virtual async Task<PagedResponse<TResponse>> GetByFiltersWithIncludesAsync(
    //         GetByFiltersRequest request,
    //         params Func<IQueryable<TEntity>, IQueryable<TEntity>>[] includes)
    //     {
    //         var filterConditions = request.Filters?.Select(f =>
    //             (f.Field, f.Operator.ToString(), f.Value)) ?? Enumerable.Empty<(string, string, string)>();

    //         var (items, totalCount) = await _repository.GetByFiltersPagedWithIncludesAsync(
    //             filterConditions,
    //             request.LogicalOperator,
    //             request.PageNumber,
    //             request.PageSize,
    //             includes);

    //         return new PagedResponse<TResponse>
    //         {
    //             Items = items.Select(ToDto).ToList(),
    //             TotalCount = totalCount,
    //             PageNumber = request.PageNumber,
    //             PageSize = request.PageSize
    //         };
    //     }

    //     public virtual async Task UpdateAsync(TResponse dto)
    //     {
    //         await _repository.UpdateAsync(ToEntity(dto));
    //     }
    // }
}
