namespace UI.Models;

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