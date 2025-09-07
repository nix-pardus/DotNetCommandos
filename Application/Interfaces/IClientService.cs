using ServiceCenter.Application.DTO.Client;

namespace ServiceCenter.Application.Interfaces;

/// <summary>
/// Сервис для работы с клиентами
/// </summary>
public interface IClientService
{
    public interface IClientService
    {
        //TODO: пока так, но в итоге надо сделать отденьные dto
        Task CreateAsync(ClientDto dto);
        Task<ClientDto> UpdateAsync(ClientDto dto);
        Task DeleteAsync(Guid id);


    /// <summary>
    /// Удаление клиента по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор клиента</param>
    /// <returns>Удалённый клиент</returns>
    Task<ClientDto> DeleteAsync(Guid id);
}
