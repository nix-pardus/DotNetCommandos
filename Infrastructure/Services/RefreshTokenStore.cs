using ServiceCenter.Domain.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Infrastructure.Services
{
    public class RefreshTokenStore: IRefreshTokenStore
    {
        private static readonly ConcurrentDictionary<string, (Guid EmployeeId, DateTime Expiry)>
            _tokens = new();
        //пусть будет 7 дней
        public string GenerateRefreshToken(Guid employeeId, int expiryDays = 7)
        {
            var token = Guid.NewGuid().ToString("N");
            var expiry = DateTime.UtcNow.AddDays(expiryDays);

            _tokens[token] = (employeeId, expiry);

            return token;
        }

        public Guid? ValidateRefreshToken(string token)
        {
            if (_tokens.TryGetValue(token, out var data))
            {
                if (data.Expiry > DateTime.UtcNow)
                {
                    return data.EmployeeId;
                }
                _tokens.TryRemove(token, out _);
            }
            return null;
        }

        public void RemoveRefreshToken(string token)
        {
            _tokens.TryRemove(token, out _);
        }
    }
}
