using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NorthWind.Sales.Backed.BusinessObjects.POCOEntities;
using NorthWind.Sales.Backend.EFCore.Entities;
using NorthWind.Sales.Backend.EFCore.Options;


namespace NorthWind.Sales.Backend.EFCore.DataContexts;

internal class NorthWindSalesContext : DbContext
{
    private readonly IOptions<DbOptions> _options;
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }

    public NorthWindSalesContext(IOptions<DbOptions> options)
    {
        _options = options;
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseSqlServer(_options.Value.ConnectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
