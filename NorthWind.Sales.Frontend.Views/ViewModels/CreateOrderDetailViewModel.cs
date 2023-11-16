namespace NorthWind.Sales.Frontend.Views.ViewModels;

public class CreateOrderDetailViewModel
{
    public int ProductId { get; set; }
    public decimal UnitPrice { get; set; }
    public short Quantity { get; set; }
}
