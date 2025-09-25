namespace ServiceCenter.Domain.ValueObjects.Enums;

/// <summary>
/// Роли сотрудников (можно назначить несколько ролей, используется аттрибут [Flags])
/// </summary>
[Flags]
public enum RoleType
{
    /// <summary>
    /// Нет роли
    /// </summary>
    None = 0,

    /// <summary>
    /// Администратор
    /// </summary>
    Administrator = 1,

    /// <summary>
    /// Выездной мастер
    /// </summary>
    Master = 2,

    /// <summary>
    /// Оператор
    /// </summary>
    Operator = 4
}
