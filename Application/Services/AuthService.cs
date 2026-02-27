using ServiceCenter.Application.DTO.Requests;
using ServiceCenter.Application.DTO.Responses;
using ServiceCenter.Application.Interfaces;
using ServiceCenter.Domain.Entities;
using ServiceCenter.Domain.Interfaces;


namespace ServiceCenter.Application.Services
{
    /// <summary>
    /// Сервис аутентификации
    /// Реализует <see cref="IAuthService"/>
    /// </summary>
    public class AuthService : IAuthService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public AuthService(
            IEmployeeRepository employeeRepository,
            IPasswordHasher passwordHasher,
            IJwtTokenService jwtTokenService,
            IRefreshTokenRepository refreshTokenRepository)
        {
            _employeeRepository = employeeRepository;
            _passwordHasher = passwordHasher;
            _jwtTokenService = jwtTokenService;
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<AuthResponse?> LoginAsync(LoginRequest request)
        {
            var employee = await _employeeRepository.GetByEmailAsync(request.Email);

            
            if (employee is null || !_passwordHasher.VerifyPassword(request.Password, employee?.PasswordHash))
                return null;

            var token = _jwtTokenService.GenerateToken(employee);
            var refreshToken = GenerateRefreshToken(employee.Id);

            await _refreshTokenRepository.AddAsync(refreshToken);

            return new AuthResponse
            {
                Token = token,
                RefreshToken = refreshToken.Token,
                ExpiresAt = DateTime.UtcNow.AddMinutes(60),
                Employee = new EmployeeMinimalResponse
                (
                    employee.Id,
                    employee.Name,
                    employee.LastName,
                    employee.Patronymic
                )
            };
        }

        public async Task<bool> ChangePasswordAsync(Guid employeeId, string currentPassword, string newPassword)
        {
            var employee = await _employeeRepository.GetByIdAsync(employeeId);
            if (employee == null) return false;

            if (!_passwordHasher.VerifyPassword(currentPassword, employee.PasswordHash))
                return false;

            employee.PasswordHash = _passwordHasher.HashPassword(newPassword);
            await _employeeRepository.UpdateAsync(employee);

            await _refreshTokenRepository.RevokeAllForUserAsync(employeeId);

            return true;
        }

        public async Task<AuthResponse?> RefreshTokenAsync(string refreshToken)
        {
            var storedToken = await _refreshTokenRepository.GetByTokenAsync(refreshToken);
            if(storedToken == null) return null;

            await _refreshTokenRepository.RevokeAsync(refreshToken);

            var employee = await _employeeRepository.GetByIdAsync(storedToken.EmployeeId);
            if (employee == null) return null;

            var newAccessToken = _jwtTokenService.GenerateToken(employee);
            var newRefreshToken = GenerateRefreshToken(employee.Id);

            await _refreshTokenRepository.AddAsync(newRefreshToken);

            return new AuthResponse
            {
                Token = newAccessToken,
                RefreshToken = newRefreshToken.Token,
                ExpiresAt = DateTime.UtcNow.AddMinutes(60),
                Employee = new EmployeeMinimalResponse
                (
                    employee.Id,
                    employee.Name,
                    employee.LastName,
                    employee.Patronymic
                )
            };
        }

        private RefreshToken GenerateRefreshToken(Guid employeeId)
        {
            return new RefreshToken
            {
                Id = Guid.NewGuid(),
                Token = Guid.NewGuid().ToString("N"),
                EmployeeId = employeeId,
                ExpiryDate = DateTime.UtcNow.AddDays(7),
                CreatedAt = DateTime.UtcNow
            };
        }
    }
}
