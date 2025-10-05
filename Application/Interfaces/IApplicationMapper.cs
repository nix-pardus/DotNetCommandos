using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.Interfaces
{
    public interface IApplicationMapper
    {
        TDestination Map<TDestination>(object source);
    }
}
