using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Domain.Interfaces
{
    public interface IFilterBuilder<T> where T : class
    {
        ISpecification<T> BuildSpecification(
            IEnumerable<(string Field, string Operator, string Value)> filters,
            string logicalOperator);
    }
}
