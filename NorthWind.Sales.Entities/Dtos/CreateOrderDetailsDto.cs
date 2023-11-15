namespace NorthWind.Sales.Entities.Dtos;

public class CreateOrderDetailsDto
{
    public CreateOrderDetailsDto(int productId, decimal unitPrice, short quantity)
    {
        ProductId = productId;
        UnitPrice = unitPrice;
        Quantity = quantity;
    }

    public int ProductId { get; }
    public decimal UnitPrice { get; }
    public short Quantity { get; }
}
