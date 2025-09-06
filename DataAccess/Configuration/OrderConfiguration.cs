using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceCenter.Domain.Entities;

namespace Infrascructure.DataAccess.Configuration;

public class OrderConfiguration:IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(x => x.Id);
    }
}
