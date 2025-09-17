using ServiceCenter.Application.DTO.Schedule;
using ServiceCenter.Application.Interfaces;
using ServiceCenter.Application.Mappers;
using ServiceCenter.Domain.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ServiceCenter.Application.Services;

/// <summary>
/// Сервис для работы с графиком работы
/// Реализует <see cref="IScheduleService"/>
/// </summary>
public class ScheduleExceptionService(IScheduleExceptionRepository repository) : IScheduleExceptionService
{
    /// <inheritdoc />
    public async Task CreateAsync(ScheduleExceptionDto dto)
    {
        await repository.AddAsync(ScheduleExceptionMapper.ToEntity(dto));
    }

        public async Task DeleteAsync(Guid id)
        {
            await repository.DeleteAsync(id);
        }

    public async Task<IEnumerable<ScheduleExceptionDto>> GetAllByEmployeePaged(Guid employeeId, int page, int pageSize)
    {
        return (await repository.GetByEmployeePaged(employeeId, page, pageSize)).Select(ScheduleExceptionMapper.ToDto);
    }

    /// <inheritdoc />
    public Task<ScheduleExceptionDto> UpdateAsync(ScheduleExceptionDto dto)
    {
        throw new NotImplementedException();
    }
}
