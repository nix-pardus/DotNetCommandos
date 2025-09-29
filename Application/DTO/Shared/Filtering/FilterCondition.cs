using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTO.Shared.Filtering
{
    public class FilterCondition
    {
        public string Field { get; set; }
        public FilterOperator Operator { get; set; }
        public string Value { get; set; }
        //Это была заготовка для каких-нибудь in range, но кмк это лишнее, если есть два разных фильтра
        //public string ValueFrom { get; set; }
        //public string ValueTo { get; set; }
        public string ValueType { get; set; }
    }
}
