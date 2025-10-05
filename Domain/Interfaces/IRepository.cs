using ServiceCenter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Domain.Interfaces
{
    public interface IRepository<T> where T : class, IEntity
    {
        Task<(List<T> Items, int TotalCount)> GetByFiltersPagedAsync(
                        IEnumerable<(string Field, string Operator, string Value)> filterConditions,
                        string logicalOperator,
                        int pageNumber,
                        int pageSize);
        Task<T> GetByIdAsync(Guid id);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(Guid id);
    }
}
