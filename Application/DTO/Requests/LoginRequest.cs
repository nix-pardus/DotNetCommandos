using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTO.Requests
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "Email обязателен")]
        [EmailAddress(ErrorMessage = "Некорректный формат email")]
        [StringLength(100, ErrorMessage = "Email не должен превышать 100 символов")]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "Пароль обязателен")]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "Пароль должен быть от 4 до 20 символов")]
        public string Password { get; set; } = string.Empty;
    }
}
