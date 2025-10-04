using ServiceCenter.Application.DTO.Requests;
using ServiceCenter.Application.DTO.Responses;

namespace ServiceCenter.Application.Interfaces;

/// <summary>
/// Сервис для работы с клиентами
/// </summary>

public interface IClientService
{
    //TODO: пока так, но в итоге надо сделать отденьные dto
    Task CreateAsync(ClientCreateRequest dto);
    Task<ClientFullResponse> UpdateAsync(ClientUpdateRequest dto);
    /// <summary>
    /// Удаление клиента по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор клиента</param>
    /// <returns>Удалённый клиент</returns>
    Task DeleteAsync(Guid id);

}
