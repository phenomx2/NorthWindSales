namespace NorthWind.Sales.Entities.Dtos;

public class CreateOrderDto
{
    public string CustomerId { get; }
    public string ShipAddress { get; set; }
    public string ShipCity { get; }
    public string ShipCountry { get; }
    public string ShipPostalCode { get; }
    public IEnumerable<CreateOrderDetailsDto> OrderDetails { get; }

    public CreateOrderDto(string customerId, string shipAddress, string shipCity, string shipCountry, string shipPostalCode, IEnumerable<CreateOrderDetailsDto> orderDetails)
    {
        CustomerId = customerId;
        ShipAddress = shipAddress;
        ShipCity = shipCity;
        ShipCountry = shipCountry;
        ShipPostalCode = shipPostalCode;
        OrderDetails = orderDetails;
    }
}
