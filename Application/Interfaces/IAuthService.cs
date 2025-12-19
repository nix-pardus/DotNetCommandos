using ServiceCenter.Application.DTO.Requests;
using ServiceCenter.Application.DTO.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponse?> LoginAsync(LoginRequest request);
        Task<bool> ChangePasswordAsync(Guid employeeId, string currentPassword, string newPassword);
    }
}
