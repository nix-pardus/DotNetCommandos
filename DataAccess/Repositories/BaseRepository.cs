using Microsoft.EntityFrameworkCore;
using ServiceCenter.Domain.Entities;
using ServiceCenter.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public virtual async Task<(List<T> Items, int TotalCount)> GetByFiltersPagedAsync(IEnumerable<(string Field, string Operator, string Value)> filterConditions, string logicalOperator, int pageNumber, int pageSize)
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

            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, totalCount);
        }

        public virtual async Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
