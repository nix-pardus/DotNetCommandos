using ServiceCenter.Application.DTO.Requests;
using ServiceCenter.Application.DTO.Responses;
using ServiceCenter.Application.Interfaces;
using ServiceCenter.Domain.Interfaces;


namespace ServiceCenter.Application.Services
{
    /// <summary>
    /// Сервис аутентификации
    /// Реализует <see cref="IAuthService"/>
    /// </summary>
    public class AuthService : IAuthService
    {
        private readonly IRefreshTokenStore _refreshTokenStore;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtTokenService _jwtTokenService;

        public AuthService(
            IEmployeeRepository employeeRepository,
            IPasswordHasher passwordHasher,
            IJwtTokenService jwtTokenService,
            IRefreshTokenStore refreshTokenStore)
        {
            _employeeRepository = employeeRepository;
            _passwordHasher = passwordHasher;
            _jwtTokenService = jwtTokenService;
            _refreshTokenStore = refreshTokenStore;
        }

        public async Task<AuthResponse?> LoginAsync(LoginRequest request)
        {
            var employee = await _employeeRepository.GetByEmailAsync(request.Email);

            
            if (employee is null || !_passwordHasher.VerifyPassword(request.Password, employee?.PasswordHash))
                return null;

            var token = _jwtTokenService.GenerateToken(employee);
            var refreshToken = _refreshTokenStore.GenerateRefreshToken(employee.Id,7);
            return new AuthResponse
            {
                Token = token,
                RefreshToken = refreshToken,
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

            return true;
        }

        public async Task<AuthResponse?> RefreshTokenAsync(string refreshToken)
        {
            var employeeId = _refreshTokenStore.ValidateRefreshToken(refreshToken);
            if (employeeId == null) return null;

            var employee = await _employeeRepository.GetByIdAsync(employeeId.Value);
            if (employee == null) return null;


            _refreshTokenStore.RemoveRefreshToken(refreshToken);

            var newAccessToken = _jwtTokenService.GenerateToken(employee);
            var newRefreshToken = _refreshTokenStore.GenerateRefreshToken(employee.Id, 7);

            return new AuthResponse
            {
                Token = newAccessToken,
                RefreshToken = newRefreshToken,
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
    }
}
