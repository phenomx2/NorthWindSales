using NorthWind.Sales.Backed.BusinessObjects.Aggregates;

namespace NorthWind.Sales.Backed.BusinessObjects.Interfaces.CreateOrder;

public interface ICreateOrderOutputPort
{
    int OrderId { get; }
    ValueTask Handle(OrderAggregate addedOrder);
}
