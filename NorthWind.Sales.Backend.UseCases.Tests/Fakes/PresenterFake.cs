using NorthWind.Sales.Backed.BusinessObjects.Aggregates;
using NorthWind.Sales.Backed.BusinessObjects.Interfaces.CreateOrder;

namespace NorthWind.Sales.Backend.UseCases.Tests.Fakes;

public class PresenterFake : ICreateOrderOutputPort
{
    public int OrderId { get; private set; }
    public OrderAggregate Order { get; set; }
    public ValueTask Handle(OrderAggregate addedOrder)
    {
        Order = addedOrder;
        OrderId = addedOrder.Id;
        return ValueTask.CompletedTask;
    }
}
