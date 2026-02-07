using ServiceCenter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Domain.Interfaces
{
    public interface IRefreshTokenStore
    {
        string GenerateRefreshToken(Guid employeeId, int expiryDays);
        Guid? ValidateRefreshToken(string token);
        void RemoveRefreshToken(string token);
    }
}