using NorthWind.Sales.Backed.BusinessObjects.ValueObjects;

namespace NorthWind.Sales.Backed.BusinessObjects.Interfaces.Repositories;

public interface IQueriesRepository
{
    Task<decimal?> GetCustomerCurrentBalance(string customerId);
    Task<IEnumerable<ProductUnitsInStock>> GetProductsUnitsInStock(IEnumerable<int> productsIds);
}
