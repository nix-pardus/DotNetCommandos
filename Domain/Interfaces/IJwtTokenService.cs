using ServiceCenter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Domain.Interfaces
{
    public interface IJwtTokenService
    {
        string GenerateToken(Employee employee);
        Guid? ValidateToken(string token);
    }
}