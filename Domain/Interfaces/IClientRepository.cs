using ServiceCenter.Domain.Entities;

namespace ServiceCenter.Domain.Interfaces;

/// <summary>
/// Репозиторий для работы с сущностью Клиент
/// </summary>
public interface IClientRepository
{
    /// <summary>
    /// Получить всех клиентов
    /// </summary>
    Task<IEnumerable<Client>> GetAllAsync();

    /// <summary>
    /// Получить клиента по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор клиента</param>
    Task<Client> GetByIdAsync(Guid id);

    /// <summary>
    /// Добавить нового клиента
    /// </summary>
    /// <param name="client">Сущность клиента</param>
    Task AddAsync(Client client);

    /// <summary>
    /// Обновить существующего клиента
    /// </summary>
    /// <param name="client">Сущность клиента</param>
    Task UpdateAsync(Client client);

    /// <summary>
    /// Удалить клиента по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор клиента</param>
    Task DeleteAsync(Guid id);
}

