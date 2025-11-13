using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceCenter.Domain.Entities;

namespace ServiceCenter.Infrascructure.DataAccess.Configuration;

public class OrderEmployeeConfiguration : IEntityTypeConfiguration<OrderEmployee>
{
    public void Configure(EntityTypeBuilder<OrderEmployee> builder)
    {
        builder.HasKey(x => x.Id);

        // Составной ключ для предотвращения дублирования назначений
        builder.HasIndex(x => new { x.OrderId, x.EmployeeId })
            .HasFilter("\"IsDeleted\" = false")
            .IsUnique();

        builder.HasOne(x => x.Order)
            .WithMany(x => x.AssignedEmployees)
            .HasForeignKey(x => x.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Employee)
            .WithMany(x => x.AssignedOrders)
            .HasForeignKey(x => x.EmployeeId)
            .OnDelete(DeleteBehavior.Cascade);

        // Ограничение: только один главный сотрудник на заказ
        builder.HasIndex(x => new { x.OrderId, x.IsPrimary})
            .HasFilter("\"IsPrimary\" = true AND \"IsDeleted\" = false")
            .IsUnique();
    }
}
