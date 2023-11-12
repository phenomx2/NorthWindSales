namespace NorthWind.Sales.Entities.Dtos;

public class CreateOrderDto
{
    public string CustomerId { get; set; }
    public string ShipAddress { get; set; }
    public string ShipCity { get; set; }
    public string ShipCountry { get; set; }
    public string ShipPostalCode { get; set; }
    public IList<CreateOrderDetailsDto> OrderDetails { get; set; }
}