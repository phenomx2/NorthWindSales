using System.Reflection;
using Microsoft.EntityFrameworkCore;
using NorthWind.Sales.Backed.BusinessObjects.POCOEntities;
using NorthWind.Sales.Backend.EFCore.Entities;

namespace NorthWind.Sales.Backend.EFCore.DataContexts;

internal class NorthWindContext : DbContext
{
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseSqlServer("Server=localhost;Database=NorthWindDB;User=sa;Password=Romero1.;TrustServerCertificate=true;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
