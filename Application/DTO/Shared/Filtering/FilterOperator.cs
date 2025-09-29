using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTO.Shared.Filtering
{
    public enum FilterOperator
    {
        Equals,
        NotEquals,
        Contains,
        StartsWith, //не реалзован
        EndsWith, //не реалзован
        GreaterThan,
        LessThan,
        GreaterThanOrEqual, //не реалзован
        LessThanOrEqual, //не реалзован
        In,//я не хочу его делать=(
        IsNull,
        IsNotNull
    }
}
