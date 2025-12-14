using ServiceCenter.Application.DTO.Employee;
using ServiceCenter.Application.DTO.Requests;
using ServiceCenter.Application.DTO.Responces;
using ServiceCenter.Application.Interfaces;
using ServiceCenter.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtTokenService _jwtTokenService;

        public AuthService(
            IEmployeeRepository employeeRepository,
            IPasswordHasher passwordHasher,
            IJwtTokenService jwtTokenService)
        {
            _employeeRepository = employeeRepository;
            _passwordHasher = passwordHasher;
            _jwtTokenService = jwtTokenService;
        }

        public async Task<AuthResponse?> LoginAsync(LoginRequest request)
        {
            var employee = await _employeeRepository.GetByEmailAsync(request.Email);

            
            if (!_passwordHasher.VerifyPassword(request.Password, employee.PasswordHash))
                return null;

            await _employeeRepository.UpdateAsync(employee);

            var token = _jwtTokenService.GenerateToken(employee);

            return new AuthResponse
            {
                Token = token,
                ExpiresAt = DateTime.UtcNow.AddMinutes(60),
                Employee = new EmployeeMinimalDto
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
    }
}
