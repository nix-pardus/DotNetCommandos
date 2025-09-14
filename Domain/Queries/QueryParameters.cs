namespace ServiceCenter.Domain.Queries
{
    /// <summary>
    /// ������� ����� ��� ���������� �������� (����� ���� �������� ��� �������������).
    /// </summary>
    public abstract class QueryParameters<TSortBy> where TSortBy : struct, Enum
    {
        /// <summary>
        /// ����� �������� ��� ��������� (�� ��������� 1).
        /// </summary>
        public int PageNumber { get; set; } = 1;
        /// <summary>
        /// ������ �������� ��� ��������� (�� ��������� 10).
        /// </summary>
        public int PageSize { get; set; } = 10;
        /// <summary>
        /// ����������� ���������� (�� ��������� �� �����������).
        /// </summary>
        public bool IsSortDescending { get; set; }
        /// <summary>
        /// ������������ ���� ��� ����������.
        /// </summary>
        public TSortBy SortBy { get; set; }
        /// <summary>
        /// ���� ��� ���������� �������� ������� (true - ������ ��������, false - ������ �� ��������, null - ��� ������).
        /// </summary>
        public bool? IsDeleted { get; set; }
        /// <summary>
        /// ���� �������� ������ ����� ��������� ���� (������������).
        /// </summary>
        public DateTime? CreatedDateAfter { get; set; }
        /// <summary>
        /// ���� �������� ������ �� ��������� ���� (������������).
        /// </summary>
        public DateTime? CreatedDateBefore { get; set; }
    }
}