namespace ServiceCenter.Domain.ValueObjects;

public enum OrderStatus
{
    /// <summary>
    /// Создан
    /// </summary>
    Created,
    /// <summary>
    /// Запланирован
    /// </summary>
    Planned,
    /// <summary>
    /// В работе
    /// </summary>
    Processing,
    /// <summary>
    /// Завершен
    /// </summary>
    Done,
    /// <summary>
    /// Отменен
    /// </summary>
    Canceled
}