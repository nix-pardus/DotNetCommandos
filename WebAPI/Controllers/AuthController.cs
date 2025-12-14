using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceCenter.Application.DTO.Assignment;
using ServiceCenter.Application.DTO.Requests;
using ServiceCenter.Application.DTO.Responces;
using ServiceCenter.Application.DTO.Shared;
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
        public async Task<ActionResult<AuthResponse>> Login(LoginRequest request)
        {
            var result = await _authService.LoginAsync(request);

            if (result == null)
                return Unauthorized("Invalid email or password");

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
    }
}

