namespace UI.Models.Employees;

public class EmployeeMinimal
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public string? Patronymic { get; set; }
    public string FullName => $"{LastName} {Name} {Patronymic}".Trim();

    public static EmployeeMinimal FromEmployee(Employee employee)
    {
        if (employee == null)
            return null;

        return new()
        {
            Id = employee.Id,
            Name = employee.Name,
            LastName = employee.LastName,
            Patronymic = employee.Patronymic
        };
    }
}
