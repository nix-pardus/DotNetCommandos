using ServiceCenter.Domain.ValueObjects.Enums;

namespace ServiceCenter.Domain.Entities;

/// <summary>
/// Сущность сотрудника сервиса.
/// Наследуется от <see cref="EntityBase"/>, который содержит общие поля для всех сущностей (например, Id, CreatedDate, ModifiedDate).
public class Employee : EntityBase
{
    /// <summary>
    /// Имя сотрудника.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Фамилия сотрудника.
    /// </summary>
    public string LastName { get; set; } = null!;

    /// <summary>
    /// Отчество сотрудника.
    /// </summary>
    public string Patronymic { get; set; } = null!;

    /// <summary>
    /// Домашний адрес сотрудника.
    /// </summary>
    public string Address { get; set; } = null!;

    /// <summary>
    /// Электронная почта сотрудника.
    /// </summary>
    public string Email { get; set; } = null!;

    /// <summary>
    /// Контактный телефон сотрудника.
    /// </summary>
    public string PhoneNumber { get; set; } = null!;

    /// <summary>
    /// Идентификатор сотрудника, который создал данного сотрудника.
    /// </summary>
    public Guid CreatedById { get; set; }

    /// <summary>
    /// Роль сотрудника в системе.
    /// </summary>
    public RoleType Role { get; set; }
}