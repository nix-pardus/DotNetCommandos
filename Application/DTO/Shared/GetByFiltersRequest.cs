using ServiceCenter.Application.DTO.Shared.Filtering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTO.Shared
{
    public class GetByFiltersRequest : PagedRequest
    {
        public string LogicalOperator { get; set; } = "AND";
        public List<Sorting> SortBy { get; set; } = new();
        public List<FilterCondition> Filters { get; set; } = new();
    }
}
