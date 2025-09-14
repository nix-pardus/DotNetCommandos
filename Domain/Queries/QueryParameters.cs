namespace ServiceCenter.Domain.Queries
{
    /// <summary>
    /// Базовый класс для параметров запросов (может быть расширен при необходимости).
    /// </summary>
    public abstract class QueryParameters<TSortBy> where TSortBy : struct, Enum
    {
        /// <summary>
        /// Номер страницы для пагинации (по умолчанию 1).
        /// </summary>
        public int PageNumber { get; set; } = 1;
        /// <summary>
        /// Размер страницы для пагинации (по умолчанию 10).
        /// </summary>
        public int PageSize { get; set; } = 10;
        /// <summary>
        /// Направление сортировки (по умолчанию по возрастанию).
        /// </summary>
        public bool IsSortDescending { get; set; }
        /// <summary>
        /// Наименование поля для сортировки.
        /// </summary>
        public TSortBy SortBy { get; set; }
        /// <summary>
        /// Флаг для фильтрации удалённых записей (true - только удалённые, false - только не удалённые, null - все записи).
        /// </summary>
        public bool? IsDeleted { get; set; }
        /// <summary>
        /// Дата создания записи после указанной даты (включительно).
        /// </summary>
        public DateTime? CreatedDateAfter { get; set; }
        /// <summary>
        /// Дата создания записи до указанной даты (включительно).
        /// </summary>
        public DateTime? CreatedDateBefore { get; set; }
    }
}