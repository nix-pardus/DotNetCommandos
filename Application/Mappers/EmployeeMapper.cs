using Riok.Mapperly.Abstractions;
using ServiceCenter.Application.DTO.Requests;
using ServiceCenter.Application.DTO.Responses;
using ServiceCenter.Domain.Entities;
using ServiceCenter.Application.DTO.OrderAssignment;
using ServiceCenter.Application.DTO.Employee;

namespace ServiceCenter.Application.Mappers;

[Mapper]
public static partial class EmployeeMapper
{
    // Mapperly автоматически создаст методы маппинга
    public static partial EmployeeDto ToDto(Employee employee);
    public static partial EmployeeMinimalResponse ToMinimalDto(Employee employee);
    public static partial Employee ToEntity(EmployeeDto dto);

    // Ручной маппер для сотрудника с назначенными заказами (OrderEmployee -> OrderAssignmentDto)
    public static EmployeeWithOrdersDto ToWithOrdersDto(Employee employee)
    {
        if (employee == null) throw new ArgumentNullException(nameof(employee));

        var assignedOrders = (employee.AssignedOrders ?? Enumerable.Empty<OrderEmployee>())
            .Where(oe => oe.Order != null) // защита на случай частичных include
            .Select(oe => new OrderAssignmentDto(
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

        return new EmployeeWithOrdersDto(
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