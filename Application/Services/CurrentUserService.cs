using Microsoft.AspNetCore.Http;
using ServiceCenter.Application.Interfaces;
using System.Security.Claims;

namespace ServiceCenter.Application.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    public Guid? UserId
    {
        get
        {
            var user = _httpContextAccessor.HttpContext.User;
            if (user?.Identity?.IsAuthenticated != true) return null;

            var idClaim = user.FindFirst(ClaimTypes.NameIdentifier) ?? user.FindFirst("sub");
            if(idClaim == null) return null;

            return Guid.TryParse(idClaim.Value, out var id) ? id : null;
        }
    }
}
