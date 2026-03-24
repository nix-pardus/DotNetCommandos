using ServiceCenter.Application.DTO.Requests;
using ServiceCenter.Application.DTO.Responses;
using ServiceCenter.Application.Interfaces;
using ServiceCenter.Application.Mappers;
using ServiceCenter.Domain.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ServiceCenter.Application.Services;

/// <summary>
/// Сервис для работы с графиком работы
/// Реализует <see cref="IScheduleService"/>
/// </summary>
public class ScheduleExceptionService(IScheduleExceptionRepository repository, ICurrentUserService currentUserService, IScheduleService scheduleService) : IScheduleExceptionService
{
    /// <inheritdoc />
    public async Task CreateAsync(ScheduleExceptionCreateRequest dto)
    {
        var entity = ScheduleExceptionMapper.ToEntity(dto);
        entity.CreatedById = currentUserService.UserId ?? Guid.Empty;
        entity.CreatedDate = DateTime.UtcNow;
        await repository.AddAsync(entity);

        scheduleService.InvalidateCacheForEmployee(dto.EmployeeId);
    }

    public async Task DeleteAsync(Guid id)
    {
        var exception = await repository.GetByIdAsync(id);
        if (exception == null)
            throw new InvalidOperationException($"Исключение с id {id} не найдено.");

        var employeeId = exception.EmployeeId;
        await repository.DeleteAsync(id);

        scheduleService.InvalidateCacheForEmployee(employeeId);
    }

    public async Task<IEnumerable<ScheduleExceptionFullResponse>> GetAllByEmployeePaged(Guid employeeId, int page, int pageSize)
    {
        return (await repository.GetByEmployeePaged(employeeId, page, pageSize)).Select(ScheduleExceptionMapper.ToResponse);
    }

    /// <inheritdoc />
    public Task<ScheduleExceptionFullResponse> UpdateAsync(ScheduleExceptionUpdateRequest dto)
    {
        throw new NotImplementedException();
    }
}
