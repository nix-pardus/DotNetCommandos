namespace UI.Models
{
    public class Employee
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public Guid CreatedById { get; set; }
        public Guid? ModifyById { get; set; }
        public RoleType Role { get; set; }
        public bool IsDeleted { get; set; }
        public string FullName { get => $"{LastName} {Name} {Patronymic}"; }
    }
}
