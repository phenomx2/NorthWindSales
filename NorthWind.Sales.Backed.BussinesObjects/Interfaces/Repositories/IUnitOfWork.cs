namespace NorthWind.Sales.Backed.BusinessObjects.Interfaces.Repositories;

public interface IUnitOfWork
{
    ValueTask SaveChanges();
}
