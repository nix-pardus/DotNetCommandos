﻿using ServiceCenter.Domain.ValueObjects.Enums;

namespace ServiceCenter.Domain.Queries;

/// <summary>
/// 
/// </summary>
public class GetEmployeesQuery : QueryParameters<EmployeeSortBy>
{
    public string? Name { get; set; }
    public string? LastName { get; set; }
    public string? Patronymic { get; set; }
    public string? Address { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public RoleType? Role { get; set; }
    public Guid? CreatedById { get; set; }
}
