using Domain.DTO.Order;

namespace Domain.Aggregates
{
    /// <summary>
    /// Заказ/заявка
    /// </summary>
    public class Order
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

        public Order(OrderDto dto)
        {
            Id = dto.Id;
            ClientId = dto.ClientId;
            MasterId = dto.MasterId;
            Device = dto.Device;
            Model = dto.Model;
            Warranty = dto.Warranty;
            ProblemDescription = dto.ProblemDescription;
            Note = dto.Note;
            Comment = dto.Comment;
            HowFind = dto.HowFind;
        }
    }
}
