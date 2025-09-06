using ServiceCenter.Application.DTO.Client;

namespace ServiceCenter.Application.Interfaces;

/// <summary>
/// Сервис для работы с клиентами
/// </summary>
public interface IClientService
{
    /// <summary>
    /// Создание нового клиента
    /// </summary>
    /// <param name="dto">DTO клиента</param>
    /// <returns>Задача выполнения операции</returns>
    Task CreateAsync(ClientDto dto);

    /// <summary>
    /// Обновление данных клиента
    /// </summary>
    /// <param name="dto">DTO клиента с обновлёнными данными</param>
    /// <returns>Обновлённый клиент</returns>
    Task<ClientDto> UpdateAsync(ClientDto dto);

    /// <summary>
    /// Удаление клиента по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор клиента</param>
    /// <returns>Удалённый клиент</returns>
    Task<ClientDto> DeleteAsync(Guid id);
}
