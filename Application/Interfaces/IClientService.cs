﻿using ServiceCenter.Application.DTO.Client;
using ServiceCenter.Application.DTO.Shared;

namespace ServiceCenter.Application.Interfaces;

/// <summary>
/// Сервис для работы с клиентами
/// </summary>

public interface IClientService
{
    //TODO: пока так, но в итоге надо сделать отденьные dto
    Task CreateAsync(ClientDto dto);
    Task UpdateAsync(ClientDto dto);
    Task<PagedResponse<ClientDto>> GetByFiltersAsync(GetByFiltersRequest request);
    /// <summary>
    /// Удаление клиента по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор клиента</param>
    /// <returns>Удалённый клиент</returns>
    Task DeleteAsync(Guid id);

}
