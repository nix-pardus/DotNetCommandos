using ServiceCenter.Application.DTO.Requests;
using ServiceCenter.Application.DTO.Responses;
using ServiceCenter.Application.DTO.Shared;

namespace ServiceCenter.Application.Interfaces;

/// <summary>
/// Сервис для работы с клиентами
/// </summary>

public interface IClientService
{
    //TODO: пока так, но в итоге надо сделать отденьные dto
    Task CreateAsync(ClientCreateRequest dto);
    Task UpdateAsync(ClientUpdateRequest dto);
    Task<PagedResponse<ClientFullResponse>> GetByFiltersAsync(GetByFiltersRequest request);
    Task<ClientFullResponse> GetByIdAsync(Guid id);
    /// <summary>
    /// Удаление клиента по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор клиента</param>
    /// <returns>Удалённый клиент</returns>
    Task DeleteAsync(Guid id);

}
