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
    /// Мастер-приемщик на складе
    /// </summary>
    MasterReceiver = 4,
    /// <summary>
    /// Диспетчер
    /// </summary>
    Dispatcher = 8,
    /// <summary>
    /// Менеджер
    /// </summary>
    Manager = 16
}