using ServiceCenter.Domain.Entities;

namespace ServiceCenter.Domain.Interfaces;

public interface IRefreshTokenRepository
{
    Task<RefreshToken?> GetByTokenAsync(string token);
    Task AddAsync(RefreshToken refreshToken);
    Task RevokeAsync(string token);
    Task RevokeAllForUserAsync(Guid employeeId);
    Task CleanupExpiredAsync(); // для удаления просроченных
}
