using NorthWind.Sales.Backed.BusinessObjects.Aggregates;
using NorthWind.Sales.Backed.BusinessObjects.Interfaces.Repositories;

namespace NorthWind.Sales.Backend.UseCases.Tests.Fakes;

public class RepositoryFake : ICommandsRepository
{
    private OrderAggregate _order;
    public ValueTask SaveChanges()
    {
        _order.Id = 1;
        return ValueTask.CompletedTask;
    }

    public ValueTask CreateOrder(OrderAggregate order)
    {
        _order = order;
        return ValueTask.CompletedTask;
    }
}
