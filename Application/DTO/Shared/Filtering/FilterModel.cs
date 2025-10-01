using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTO.Shared.Filtering
{
    public class FilterModel
    {
        public List<FilterCondition> Filters { get; set; } = new();
        public string LogicalOperator { get; set; } = "AND";
    }
}
