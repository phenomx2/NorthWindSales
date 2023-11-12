using NorthWind.Sales.Entities.Dtos;

namespace NorthWind.Sales.Backed.BusinessObjects.Interfaces.CreateOrder;

public interface ICreateOrderInputPort
{
    ValueTask Handle(CreateOrderDto order);
}
