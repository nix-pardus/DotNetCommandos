using Domain.DTO.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Aggregates
{
    public class Client
    {
        public Guid Id { get; set; }//Я всё-равно не считаю long хорошим выбором для ID
        public string Name { get; set; }
        public string LastName { get; set; }
        public string? Patronymic { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Region { get; set; }
        public string? CompanyName { get; set; }
        public string? Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsDeleted { get; set; } = false;

        //Конструктор для EF
        public Client() { }
        public Client(ClientDto dto)
        {
            Id = dto.Id;
            Name = dto.Name;
            LastName = dto.LastName;
            Patronymic = dto.Patronymic;
            Address = dto.Address;
            City = dto.City;
            Region = dto.Region;
            CompanyName = dto.CompanyName;
            Email = dto.Email;
            PhoneNumber = dto.PhoneNumber;
            IsDeleted = dto.IsDeleted;
        }

    }
}
