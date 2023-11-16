using NorthWind.Sales.Entities.Dtos;
using NorthWind.Sales.Frontend.BusinessObjects.Interfaces;
using NorthWind.Sales.Frontend.Views.Resources;

namespace NorthWind.Sales.Frontend.Views.ViewModels;

public class CreateOrderViewModel
{
    private readonly ICreateOrderGateway _gateway;

    public CreateOrderViewModel(ICreateOrderGateway gateway)
    {
        _gateway = gateway;
    }

    public string CustomerId { get; set; }
    public string ShipAddress { get; set; }
    public string ShipCity { get; set; }
    public string ShipCountry { get; set; }
    public string ShipPostalCode { get; set; }
    public IList<CreateOrderDetailViewModel> OrderDetails { get; set; } = new List<CreateOrderDetailViewModel>();

    public string InformationMessage { get; private set; }

    public void AddNewOrderDetailItem()
    {
        OrderDetails.Add(new CreateOrderDetailViewModel());
    }

    public async Task Send()
    {
        InformationMessage = string.Empty;
        var orderId = await _gateway.CreateOrderAsync((CreateOrderDto)this);
        InformationMessage = string.Format(CreateOrderMessages.CreatedOrderTemplate, orderId);
    }

    public static explicit operator CreateOrderDto(CreateOrderViewModel vm) =>
            new CreateOrderDto(vm.CustomerId, 
                vm.ShipAddress, 
                vm.ShipCity, 
                vm.ShipCountry,
                vm.ShipPostalCode,
                vm.OrderDetails
                    .Select(d => new CreateOrderDetailsDto(d.ProductId, d.UnitPrice, d.Quantity)));
}

