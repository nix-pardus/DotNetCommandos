using Riok.Mapperly.Abstractions;
using ServiceCenter.Application.DTO.Requests;
using ServiceCenter.Application.DTO.Responses;
using ServiceCenter.Domain.Entities;

namespace ServiceCenter.Application.Mappers;

[Mapper]
public static partial class EmployeeMapper
{
    // Mapperly автоматически создаст методы маппинга
    public static partial EmployeeFullResponse ToResponse(Employee employee);
    public static partial EmployeeMinimalResponse ToMinimalDto(Employee employee);
    public static partial Employee ToEntity(EmployeeCreateRequest dto);
    public static partial Employee ToEntity(EmployeeUpdateRequest dto);

    // Ручной маппер для сотрудника с назначенными заказами (OrderEmployee -> OrderAssignmentDto)
    public static EmployeeWithOrdersResponse ToWithOrdersDto(Employee employee)
    {
        if (employee == null) throw new ArgumentNullException(nameof(employee));

        var assignedOrders = (employee.AssignedOrders ?? Enumerable.Empty<OrderEmployee>())
            .Where(oe => oe.Order != null) // защита на случай частичных include
            .Select(oe => new OrderAssignmentResponse(
                oe.Id,
                OrderMapper.ToResponse(oe.Order),
                oe.IsPrimary,
                oe.CreatedDate,
                oe.ModifiedDate,
                oe.IsDeleted,
                oe.CreatedById,
                oe.ModifiedById
            ))
            .ToList();

        return new EmployeeWithOrdersResponse(
            employee.Id,
            employee.Name,
            employee.LastName,
            employee.Patronymic,
            employee.Address,
            employee.Email,
            employee.PhoneNumber,
            employee.CreatedDate,
            employee.CreatedById,
            employee.ModifiedDate,
            employee.ModifiedById,
            employee.Role,
            employee.IsDeleted,
            assignedOrders
        );
    }
}