using ServiceCenter.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Infrascructure.DataAccess.Specifications
{
    public class FilterBuilder<T> : IFilterBuilder<T> where T : class
    {
        public ISpecification<T> BuildSpecification(
            IEnumerable<(string Field, string Operator, string Value)> filters,
            string logicalOperator)
        {
            var result = new DynamicSpecification<T>(filters, logicalOperator);
            return result;
        }
    }
}
