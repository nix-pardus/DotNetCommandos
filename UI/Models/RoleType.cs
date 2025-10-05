namespace UI.Models;

[Flags]
public enum RoleType
{
    /// <summary>
    /// Нет роли
    /// </summary>
    Нет = 0,

    /// <summary>
    /// Администратор
    /// </summary>
    Администратор = 1,

    /// <summary>
    /// Выездной мастер
    /// </summary>
    Мастер = 2,

    /// <summary>
    /// Оператор
    /// </summary>
    Оператор = 4
}