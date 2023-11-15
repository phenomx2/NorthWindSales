using NorthWind.Sales.Backed.BusinessObjects.POCOEntities;

namespace NorthWind.Sales.Backend.EFCore.Entities;

internal class OrderDetail
{
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public decimal UnitPrice { get; set; }
    public short Quantity { get; set; }
    //A proposito se esta utilizando Order de POCO entities,
    //La regla de la dependencia me lo permite 
    public Order Order { get; set; }
}