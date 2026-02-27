using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceCenter.Application.DTO.Requests;
using ServiceCenter.Application.DTO.Responses;
using ServiceCenter.Application.Interfaces;
using System.Security.Claims;

namespace ServiceCenter.WebAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponse>> Login([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                // Вернет 400 BadRequest с ошибками
                return BadRequest(ModelState);
            }
            var result = await _authService.LoginAsync(request);

            if (result == null)
                return Unauthorized("Неверный email или пароль");

            Response.Cookies.Append("refreshToken", result.RefreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTimeOffset.UtcNow.AddDays(7)
            });

            return Ok(new
            {
                result.Token,
                result.ExpiresAt,
                result.Employee
            });
        }

        [HttpPost("change-password")]
        [Authorize]
        public async Task<ActionResult> ChangePassword(ChangePasswordRequest request)
        {
            var employeeId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var result = await _authService.ChangePasswordAsync(
                employeeId,
                request.CurrentPassword,
                request.NewPassword
            );

            if (!result)
                return BadRequest("Current password is incorrect");

            return Ok("Password changed successfully");
        }

        [HttpPost("refresh")]
        [ProducesResponseType(typeof(AuthResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            if(string.IsNullOrEmpty(refreshToken))
                return Unauthorized();

            var result = await _authService.RefreshTokenAsync(refreshToken);
            if (result == null)
            {
                Response.Cookies.Delete("refreshToken");
                return Unauthorized();
            }

            Response.Cookies.Append("refreshToken", result.RefreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddDays(7)
            });

            return Ok(new
            {
                result.Token,
                result.ExpiresAt,
                result.Employee
            });
        }
    }
}

