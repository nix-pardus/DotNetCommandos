using static System.Net.Mime.MediaTypeNames;

namespace Domain.Entities
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
        //timestamp StartDate //???
        //timestamp EndDate //???
    }
}
