using NorthWind.Sales.Backed.BusinessObjects.POCOEntities;
using NorthWind.Sales.Backed.BusinessObjects.ValueObjects;
using NorthWind.Sales.Entities.Dtos;

namespace NorthWind.Sales.Backed.BusinessObjects.Aggregates;

public class OrderAggregate : Order
{
    private readonly List<OrderDetail> _orderDetails = new List<OrderDetail>();

    public IReadOnlyCollection<OrderDetail> OrderDetails => _orderDetails;

    public void AddDetail(int productId, decimal unitPrice, short quantity)
    {
        var existingOrderDetail = _orderDetails
            .FirstOrDefault(x => x.ProductId == productId);

        if (existingOrderDetail != default)
        {
            quantity += existingOrderDetail.Quantity;
            _orderDetails.Remove(existingOrderDetail);
            _orderDetails.Add(new OrderDetail(productId, unitPrice, quantity));
        }
    }

    public static OrderAggregate FromDto(CreateOrderDto dto)
    {
        var orderAggregate = new OrderAggregate()
        {
            CustomerId = dto.CustomerId,
            ShipAddress = dto.ShipAddress,
            ShipCity = dto.ShipCity,
            ShipCountry = dto.ShipCountry,
            ShipPostalCode = dto.ShipPostalCode
        };

        foreach (var item in dto.OrderDetails)
        {
            orderAggregate.AddDetail(item.ProductId, item.UnitPrice, item.Quantity);
        }

        return orderAggregate;
    }
}
