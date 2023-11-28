using Microsoft.EntityFrameworkCore;
using NorthWind.Sales.Backed.BusinessObjects.Aggregates;
using NorthWind.Sales.Backed.BusinessObjects.Exceptions;
using NorthWind.Sales.Backed.BusinessObjects.Interfaces.Repositories;
using NorthWind.Sales.Backend.EFCore.DataContexts;

namespace NorthWind.Sales.Backend.EFCore.Repositories;

internal class CommandsRepository : ICommandsRepository
{

    private readonly NorthWindSalesContext _context;

    public CommandsRepository(NorthWindSalesContext context)
    {
        _context = context;
    }

    public async ValueTask SaveChanges()
    {
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            throw new UnitOfWorkException(ex, ex.Entries.Select(e => e.Entity.GetType().Name));
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async ValueTask CreateOrder(OrderAggregate order)
    {
        await _context.AddAsync(order);
            // ReSharper disable once CoVariantArrayConversion
        await _context.AddRangeAsync(order.OrderDetails.Select(o => new Entities.OrderDetail
        {
            Order = order,
            ProductId = o.ProductId,
            Quantity = o.Quantity,
            UnitPrice = o.UnitPrice
        }).ToArray());
    }
}
