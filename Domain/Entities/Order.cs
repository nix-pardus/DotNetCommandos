namespace Domain.Entities
{
    /// <summary>
    /// Заказ/заявка
    /// </summary>
    internal class Order
    {
        /// <summary>
        /// Идентификатор заказа/заявки
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Идентификатор клиента
        /// </summary>
        public Guid ClientId { get; set; }
        /// <summary>
        /// Идентификатор мастера
        /// </summary>
        public Guid? MasterId { get; set; }
        /// <summary>
        /// Прибор
        /// </summary>
        public string Device { get; set; }
        /// <summary>
        /// Модель
        /// </summary>
        public string? Model { get; set; }
        /// <summary>
        /// Гарантийный/негарантийный
        /// </summary>
        public bool Warranty { get; set; }
        /// <summary>
        /// Описание проблемы
        /// </summary>
        public string ProblemDescription { get; set; }
        /// <summary>
        /// Примечание
        /// </summary>
        public string? Note { get; set; }
        /// <summary>
        /// Комментарий к заказу
        /// </summary>
        public string? Comment { get; set; }
        /// <summary>
        /// Откуда узнали о нас
        /// </summary>
        public string? HowFind { get; set; }
    }
}
