using NorthWind.Sales.Backed.BusinessObjects.Aggregates;
using NorthWind.Sales.Backed.BusinessObjects.Interfaces.CreateOrder;

namespace NorthWind.Sales.Backend.Presenters;

internal class CreateOrderPresenter : ICreateOrderOutputPort
{
    public int OrderId { get; private set; }
    public ValueTask Handle(OrderAggregate addedOrder)
    {
        OrderId = addedOrder.Id;
        return ValueTask.CompletedTask;
    }
}