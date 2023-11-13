using NorthWind.Sales.Backed.BusinessObjects.Aggregates;

namespace NorthWind.Sales.Backed.BusinessObjects.Interfaces.Repositories;

public interface ICommandsRepository : IUnitOfWork
{
    ValueTask CreateOrder(OrderAggregate order);
}
