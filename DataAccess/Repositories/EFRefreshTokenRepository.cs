using Microsoft.EntityFrameworkCore;
using ServiceCenter.Domain.Entities;
using ServiceCenter.Domain.Interfaces;

namespace ServiceCenter.Infrascructure.DataAccess.Repositories;

public class EFRefreshTokenRepository : IRefreshTokenRepository
{
    private readonly DataContext context;
    public EFRefreshTokenRepository(DataContext context)
    {
        this.context = context;
    }
    public async Task<RefreshToken?> GetByTokenAsync(string token)
    {
        return await context.RefreshTokens
            .FirstOrDefaultAsync(rt => rt.Token == token && rt.RevokedAt == null && rt.ExpiryDate > DateTime.UtcNow);
    }

    public async Task AddAsync(RefreshToken refreshToken)
    {
        await context.RefreshTokens.AddAsync(refreshToken);
        await context.SaveChangesAsync();
    }
    public async Task RevokeAsync(string token)
    {
        var rt = await context.RefreshTokens.FirstOrDefaultAsync(x => x.Token == token);
        if(rt != null)
        {
            rt.RevokedAt = DateTime.UtcNow;
            await context.SaveChangesAsync();
        }
    }

    public async Task RevokeAllForUserAsync(Guid employeeId)
    {
        var tokens = await context.RefreshTokens
            .Where(rt => rt.EmployeeId == employeeId && rt.RevokedAt == null)
            .ToListAsync();

        foreach(var token in tokens)
        {
            token.RevokedAt = DateTime.UtcNow;
        }

        await context.SaveChangesAsync();
    }

    public async Task CleanupExpiredAsync()
    {
        var expired = await context.RefreshTokens
            .Where(rt => rt.ExpiryDate <= DateTime.UtcNow)
            .ToListAsync();

        context.RefreshTokens.RemoveRange(expired);
        await context.SaveChangesAsync();
    }
}
