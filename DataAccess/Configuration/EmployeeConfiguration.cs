using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceCenter.Domain.Entities;

namespace ServiceCenter.Infrascructure.DataAccess.Configuration;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
        builder.Property(x => x.LastName).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Patronymic).HasMaxLength(100);
        builder.Property(x => x.Email).IsRequired().HasMaxLength(254);
        builder.HasMany(x=>x.Schedules)
            .WithOne(x=>x.Employee)
            .HasForeignKey(x=>x.EmployeeId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
