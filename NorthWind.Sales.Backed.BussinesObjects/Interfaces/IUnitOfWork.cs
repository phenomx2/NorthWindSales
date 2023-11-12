namespace NorthWind.Sales.Backed.BusinessObjects.Interfaces;

public interface IUnitOfWork
{
    ValueTask SaveChanges();
}
