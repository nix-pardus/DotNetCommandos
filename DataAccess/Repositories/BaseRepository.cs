using Microsoft.EntityFrameworkCore;
using ServiceCenter.Domain.Entities;
using ServiceCenter.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Infrascructure.DataAccess.Repositories
{
    public class BaseRepository<T>(DataContext context, IFilterBuilder<T> filterBuilder) : IRepository<T> where T : class, IEntity
    {
        protected readonly DataContext _context = context;
        protected readonly IFilterBuilder<T> _filterBuilder = filterBuilder;
        public virtual async Task AddAsync(T entity)
        {
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(Guid id)
        {

            var client = await _context.Set<T>().FindAsync(id);
            if (client != null)
            {
                client.IsDeleted = true;
            }
            else
            {
                throw new ArgumentNullException(@$"Entity ""{id}"" is not exists");
            }
            await _context.SaveChangesAsync();
        }

        public virtual async Task<T> GetByIdAsync(Guid id)
        {
            return await _context.Set<T>().SingleOrDefaultAsync(x => x.Id == id);
        }

        public virtual async Task<(List<T> Items, int TotalCount)> GetByFiltersPagedAsync(
            IEnumerable<(string Field, string Operator, string Value)> filterConditions,
            string logicalOperator,
            int pageNumber,
            int pageSize,
            IEnumerable<(string Field, bool Descending)>? sortDefinitions = null)
        {
            var query = _context.Set<T>().AsQueryable();
            if (filterConditions?.Any() == true)
            {
                var specification = _filterBuilder.BuildSpecification(
                    filterConditions,
                    logicalOperator ?? "AND");

                query = query.Where(specification.Criteria);
            }

            var totalCount = await query.CountAsync();

            query = ApplySorting(query, sortDefinitions);

            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, totalCount);
        }

        public virtual async Task<(List<T> Items, int TotalCount)> GetByFiltersPagedWithIncludesAsync(
            IEnumerable<(string Field, string Operator, string Value)> filterConditions,
            string logicalOperator,
            int pageNumber,
            int pageSize,
            IEnumerable<(string Field, bool Descending)>? sortDefinitions = null,
            params Func<IQueryable<T>, IQueryable<T>>[] includes)
        {
            var query = _context.Set<T>().AsQueryable();

            if (filterConditions?.Any() == true)
            {
                var specification = _filterBuilder.BuildSpecification(
                    filterConditions,
                    logicalOperator ?? "AND");

                query = query.Where(specification.Criteria);
            }

            foreach (var include in includes ?? Array.Empty<Func<IQueryable<T>, IQueryable<T>>>())
            {
                query = include(query);
            }

            var totalCount = await query.CountAsync();

            query = ApplySorting(query, sortDefinitions);

            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();
            return (items, totalCount);
        }

        private IQueryable<T> ApplySorting(IQueryable<T> query, IEnumerable<(string Field, bool Descending)>? sortDefinitions)
        {
            if (sortDefinitions == null || !sortDefinitions.Any())
                return query;

            bool first = true;
            foreach (var sd in sortDefinitions)
            {
                if (string.IsNullOrWhiteSpace(sd.Field))
                    continue;

                var propInfo = typeof(T).GetProperty(sd.Field, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                if (propInfo == null)
                    continue;

                var parameter = Expression.Parameter(typeof(T), "x");
                Expression propertyAccess = Expression.Property(parameter, propInfo);
                var lambda = Expression.Lambda(propertyAccess, parameter);

                string methodName;
                if(first)
                    methodName = sd.Descending ? "OrderByDescending" : "OrderBy";
                else
                    methodName = sd.Descending ? "ThenByDescending" : "ThenBy";

                var method = typeof(Queryable).GetMethods(BindingFlags.Public | BindingFlags.Static)
                    .Where(m => m.Name == methodName && m.GetParameters().Length == 2)
                    .Single()
                    .MakeGenericMethod(typeof(T), propInfo.PropertyType);

                query = (IQueryable<T>)method.Invoke(null, new object[] { query, lambda });
                first = false;
            }

            return query;
        }

        public virtual async Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
