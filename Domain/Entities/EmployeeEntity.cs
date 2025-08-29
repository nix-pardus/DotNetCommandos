using Domain.ValueObjects.Enums;

namespace Domain.Entities;
/// <summary>
/// Сущность сотрудника
/// </summary>
public class EmployeeEntity
{
    /// <summary>
    /// Id
    /// </summary>
    public Guid Id { get; set; }
    /// <summary>
    /// Имя
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// Фамилия
    /// </summary>
    public string LastName { get; set; }
    /// <summary>
    /// Отчество
    /// </summary>
    public string Patronymic { get; set; }
    /// <summary>
    /// Домашний адрес
    /// </summary>
    public string Address { get; set; }
    /// <summary>
    /// Электронная почта
    /// </summary>
    public string Email { get; set; }
    /// <summary>
    /// Номер телефона
    /// </summary>
    public string PhoneNumber { get; set; }
    /// <summary>
    /// Дата создания сотрудника
    /// </summary>
    public DateTime CreatedDate { get; set; }
    /// <summary>
    /// Id сотрудника, создавшего этого сотрудника
    /// </summary>
    public Guid Creator { get; set; }
    /// <summary>
    /// Роль в системе
    /// </summary>
    public RoleType Role { get; set; }
    /// <summary>
    /// Актуальность сотрудника
    /// </summary>
    public bool IsDeleted { get; set; }
}
