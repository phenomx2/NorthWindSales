using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NorthWind.Sales.Backend.EFCore.Entities;

namespace NorthWind.Sales.Backend.EFCore.Configurations;

internal class OrderDetailConfiguration : IEntityTypeConfiguration<Entities.OrderDetail>
{
    public void Configure(EntityTypeBuilder<OrderDetail> builder)
    {
        builder.HasKey(e => new { e.OrderId, e.ProductId });
        builder.Property(e => e.UnitPrice)
            .HasPrecision(8, 2);
    }
}