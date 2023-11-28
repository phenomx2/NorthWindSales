using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NorthWind.Sales.Backed.BusinessObjects.POCOEntities;
using NorthWind.Sales.Backend.EFCore.Entities;

namespace NorthWind.Sales.Backend.EFCore.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.CustomerId)
            .IsRequired()
            .HasMaxLength(5)
            .IsFixedLength();
        
        builder.Property(e => e.ShipAddress)
            .IsRequired()
            .HasMaxLength(60);

        builder.Property(e => e.ShipCity)
            .HasMaxLength(15);

        builder.Property(e => e.ShipCountry)
            .HasMaxLength(15);

        builder.Property(e => e.ShipPostalCode)
            .HasMaxLength(10);

        builder.HasOne<Customer>()
            .WithMany()
            .HasForeignKey(o => o.CustomerId);
    }
}
