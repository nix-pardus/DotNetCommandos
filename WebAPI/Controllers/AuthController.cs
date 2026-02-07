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

            return Ok(result);
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
        [Authorize]
        [ProducesResponseType(typeof(RefreshTokenRequest), StatusCodes.Status200OK)]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            var result = await _authService.RefreshTokenAsync(request.RefreshToken);

            if (result == null)
                return Unauthorized();

            return Ok(result);
        }
    }
}

