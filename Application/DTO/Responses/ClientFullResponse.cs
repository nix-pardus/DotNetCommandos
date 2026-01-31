namespace ServiceCenter.Application.DTO.Responses;

/// <summary>
/// Клиент
/// </summary>
/// <param name="Id">Идентификатор клиента</param>
/// <param name="Name">Имя</param>
/// <param name="LastName">Фамилия</param>
/// <param name="Patronymic">Отчество</param>
/// <param name="Address">Адрес</param>
/// <param name="City">Город</param>
/// <param name="Region">Регион</param>
/// <param name="CompanyName">Название компании</param>
/// <param name="Email">Электронная почта</param>
/// <param name="PhoneNumber">Телефон</param>
/// <param name="IsDeleted">Признак удаления</param>
public record ClientFullResponse(
    Guid Id,
    string Name,
    string LastName,
    string Patronymic,
    string Address,
    string City,
    string Region,
    string CompanyName,
    string Email,
    string PhoneNumber,
    string? Lead,
    Guid CreatedById,
    Guid? ModifiedById,
    DateTime CreatedDate,
    DateTime? ModifiedDate,
    bool IsDeleted
);
