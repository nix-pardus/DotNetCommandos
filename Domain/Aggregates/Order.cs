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
        /// Дата/время создания заказа
        /// </summary>
        public DateTime CreatedDate { get; set; }
        /// <summary>
        /// Идентификатор создавшего заказ работника
        /// </summary>
        public long CreateBy { get; set; }
        /// <summary>
        /// Дата изменния заказа
        /// </summary>
        public DateTime ModifyDate { get; set; }
        /// <summary>
        /// Идентификатор работника, изменившего заказ
        /// </summary>
        public long ModifyBy { get; set; }
        /// <summary>
        /// Идентификатор клиента
        /// </summary>
        public Guid ClientId { get; set; }
        /// <summary>
        /// Прибор
        /// </summary>
        public string EquipmentType { get; set; }
        /// <summary>
        /// Модель
        /// </summary>
        public string? EquipmentModel { get; set; }
        /// <summary>
        /// Гарантийный/негарантийный
        /// </summary>
        public bool IsWarranty { get; set; }
        /// <summary>
        /// Описание проблемы
        /// </summary>
        public string Problem { get; set; }
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
        public string? Lead { get; set; }
        /// <summary>
        /// Приоритет
        /// </summary>
        public int Proirity { get; set; }
        /// <summary>
        /// Признак "Запись удалена"
        /// </summary>
        public bool IsDeleted { get; set; }
        /// <summary>
        /// Дата время визита мастера
        /// </summary>
        public DateTime VisitTime { get; set; }

        public Order(OrderDto dto)
        {
            Id = dto.Id;
            CreatedDate = dto.CreatedDate;
            ClientId = dto.ClientId;
            CreateBy = dto.CreateBy;
            ModifyDate = dto.ModifyDate;
            ModifyBy = dto.ModifyBy;

            EquipmentType = dto.EquipmentType;
            EquipmentModel = dto.EquipmentModel;
            IsWarranty = dto.IsWarranty;
            Problem = dto.Problem;
            Note = dto.Note;
            Comment = dto.Comment;
            Lead = dto.Lead;
            Proirity = dto.Proirity;
            IsDeleted = dto.IsDeleted;
            VisitTime = dto.VisitTime;
        }
        public Order() { }
    }
}
