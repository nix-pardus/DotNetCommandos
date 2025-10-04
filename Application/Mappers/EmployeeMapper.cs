using Riok.Mapperly.Abstractions;
using ServiceCenter.Application.DTO.Requests;
using ServiceCenter.Application.DTO.Responses;
using ServiceCenter.Domain.Entities;

namespace ServiceCenter.Application.Mappers;

[Mapper]
public static partial class EmployeeMapper
{
    // Mapperly автоматически создаст методы маппинга
    public static partial EmployeeFullResponse ToDto(Employee employee);
    public static partial EmployeeMinimalResponse ToMinimalDto(Employee employee);
    public static partial Employee ToEntity(EmployeeCreateRequest dto);
    public static partial Employee ToEntity(EmployeeUpdateRequest dto);

}