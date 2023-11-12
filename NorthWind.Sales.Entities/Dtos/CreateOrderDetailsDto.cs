namespace NorthWind.Sales.Entities.Dtos;

public class CreateOrderDetailsDto
{
    public int ProductId { get; set; }
    public decimal UnitPrice { get; set; }
    public short Quantity { get; set; }
}
