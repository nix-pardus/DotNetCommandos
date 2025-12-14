using ServiceCenter.Domain.Entities;

namespace ServiceCenter.Domain.Interfaces;

/// <summary>
/// Репозиторий для работы с сущностью Сотрудник
/// </summary>
public interface IEmployeeRepository : IRepository<Employee>
{
    Task<Employee> GetByEmailAsync(string email);
}
