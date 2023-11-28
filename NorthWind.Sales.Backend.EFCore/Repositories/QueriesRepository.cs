using Microsoft.EntityFrameworkCore;
using NorthWind.Sales.Backed.BusinessObjects.Interfaces.Repositories;
using NorthWind.Sales.Backed.BusinessObjects.ValueObjects;
using NorthWind.Sales.Backend.EFCore.DataContexts;

namespace NorthWind.Sales.Backend.EFCore.Repositories;

internal class QueriesRepository : IQueriesRepository
{
    private readonly NorthWindSalesContext _context;

    public QueriesRepository(NorthWindSalesContext context)
    {
        _context = context;
    }

    public async Task<decimal?> GetCustomerCurrentBalance(string customerId)
    {
        var result = await _context.Customers
            .FirstOrDefaultAsync(c => c.Id == customerId);
        return result?.CurrentBalance;
    }

    public async Task<IEnumerable<ProductUnitsInStock>> GetProductsUnitsInStock(IEnumerable<int> productsIds)
    {
        return await _context
            .Products
            .Where(p => productsIds.Contains(p.Id))
            .Select(p => new ProductUnitsInStock(p.Id, p.UnitsInStock))
            .ToListAsync();
    }
}
