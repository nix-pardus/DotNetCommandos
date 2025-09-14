using ServiceCenter.Domain.Queries;
using ServiceCenter.Infrascructure.DataAccess;
using System.Linq.Expressions;


public abstract class BaseRepository<TEntity> where TEntity : class
{
    protected readonly DataContext _context;

    protected BaseRepository(DataContext context)
    {
        _context = context;
    }

    protected virtual IQueryable<TEntity> ApplyPaging<TSortBy>(IQueryable<TEntity> query, QueryParameters<TSortBy> parameters)
        where TSortBy : struct, Enum
    {
        return query
            .Skip((parameters.PageNumber - 1) * parameters.PageSize)
            .Take(parameters.PageSize);
    }

    protected virtual IQueryable<TEntity> ApplySorting<TEntity, TSortBy>(
        IQueryable<TEntity> query,
        TSortBy? sortBy,
        bool isSortDescending,
        Dictionary<TSortBy, Expression<Func<TEntity, object>>> sortSelectors)
        where TSortBy : struct, Enum
    {
        if (sortBy.HasValue && sortSelectors.TryGetValue(sortBy.Value, out var selector))
        {
            query = isSortDescending
                ? query.OrderByDescending(selector)
                : query.OrderBy(selector);
        }
        return query;
    }

    protected virtual IQueryable<TEntity> ApplyFiltering(IQueryable<TEntity> query, Expression<Func<TEntity, bool>>? filter)
    {
        return filter != null ? query.Where(filter) : query;
    }
}