using ServiceCenter.Application.DTO.Requests;
using ServiceCenter.Application.DTO.Responses;

namespace ServiceCenter.Application.Interfaces
{
    /// <summary>
    /// Сервис аутентификации
    /// </summary>
    public interface IAuthService
    {
        Task<AuthResponse?> LoginAsync(LoginRequest request);
        Task<bool> ChangePasswordAsync(Guid employeeId, string currentPassword, string newPassword);
        Task<AuthResponse?> RefreshTokenAsync(string refreshToken);
    }
}
