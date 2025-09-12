namespace ServiceCenter.Domain.Entities;

/// <summary>
/// Сущность клиента сервиса.
/// Наследуется от <see cref="EntityBase"/>, который содержит общие поля для всех сущностей (например, Id, CreatedDate, ModifiedDate).
/// </summary>
public class Client : EntityBase
{
    /// <summary>
    /// Имя клиента.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Фамилия клиента.
    /// </summary>
    public string LastName { get; set; } = null!;

    /// <summary>
    /// Отчество клиента.
    /// </summary>
    public string Patronymic { get; set; } = null!;

    /// <summary>
    /// Адрес проживания клиента.
    /// </summary>
    public string Address { get; set; } = null!;

    /// <summary>
    /// Город проживания клиента.
    /// </summary>
    public string City { get; set; } = null!;

    /// <summary>
    /// Регион/область проживания клиента.
    /// </summary>
    public string Region { get; set; } = null!;

    /// <summary>
    /// Название компании клиента, если применимо.
    /// </summary>
    public string CompanyName { get; set; } = null!;

    /// <summary>
    /// Электронная почта клиента.
    /// </summary>
    public string Email { get; set; } = null!;

    /// <summary>
    /// Контактный телефон клиента.
    /// </summary>
    public string PhoneNumber { get; set; } = null!;
}
