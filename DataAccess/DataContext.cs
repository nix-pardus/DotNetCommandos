using Infrascructure.DataAccess.Configuration;
using Microsoft.EntityFrameworkCore;
using ServiceCenter.Domain.Entities;
using ServiceCenter.Infrascructure.DataAccess.Configuration;


namespace ServiceCenter.Infrascructure.DataAccess
{
    public class DataContext(DbContextOptions<DataContext> options)
                : DbContext(options)
    {
        //TODO: нужен набор для заказа
        public DbSet<Client> Clients { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<ScheduleException> ScheduleExceptions { get; set; }
        public DbSet<OrderEmployee> OrderEmployees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new ClientConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
            modelBuilder.ApplyConfiguration(new ScheduleConfiguration());
            modelBuilder.ApplyConfiguration(new ScheduleExceptionConfiguration());
            modelBuilder.ApplyConfiguration(new OrderEmployeeConfiguration());
        }
    }
}
