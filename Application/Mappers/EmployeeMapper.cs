using Riok.Mapperly.Abstractions;
using ServiceCenter.Application.DTO.Employee;
using ServiceCenter.Domain.Entities;

namespace ServiceCenter.Application.Mappers;

[Mapper]
public static partial class EmployeeMapper
{
    // Mapperly автоматически создаст методы маппинга
    public static partial EmployeeDto ToDto(Employee employee);
    public static partial Employee ToEntity(EmployeeDto dto);
}