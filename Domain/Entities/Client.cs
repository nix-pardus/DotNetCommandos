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
    public string? LastName { get; set; }

    /// <summary>
    /// Отчество клиента.
    /// </summary>
    public string? Patronymic { get; set; }

    /// <summary>
    /// Адрес проживания клиента.
    /// </summary>
    public string Address { get; set; } = null!;

    /// <summary>
    /// Город проживания клиента.
    /// </summary>
    public string City { get; set; } = null!;

    /// <summary>
    /// Район/участок/квартал в городе проживания клиента.
    /// </summary>
    public string? Region { get; set; }

    /// <summary>
    /// Название компании клиента, если применимо.
    /// </summary>
    public string? CompanyName { get; set; }

    /// <summary>
    /// Электронная почта клиента.
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// Контактный телефон клиента.
    /// </summary>
    public string PhoneNumber { get; set; } = null!;

    /// <summary>
    /// Откуда клиент узнал о сервисном центре.
    /// </summary>
    public string? Lead { get; set; }

    /// <summary>
    /// Сотрудник, создавший запись о клиенте.
    /// </summary>
    public Guid CreatedById { get; set; }

    /// <summary>
    /// Сотрудник, последний изменивший запись о клиенте.
    /// </summary>
    public Guid? ModifiedById { get; set; }
}
