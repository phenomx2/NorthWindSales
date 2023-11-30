using NorthWind.Sales.Entities.Dtos;
using NorthWind.Sales.Entities.Interfaces.Common;
using NorthWind.Sales.Entities.ValueObjects;
using NorthWind.Sales.Frontend.BusinessObjects.Interfaces;
using NorthWind.Sales.Frontend.Views.Components;
using NorthWind.Sales.Frontend.Views.Resources;

namespace NorthWind.Sales.Frontend.Views.ViewModels;

public class CreateOrderViewModel
{
    private readonly ICreateOrderGateway _gateway;

    public CreateOrderViewModel(ICreateOrderGateway gateway, IModelValidator<CreateOrderViewModel> validator)
    {
        _gateway = gateway;
        Validator = validator;
    }

    public string CustomerId { get; set; }
    public string ShipAddress { get; set; }
    public string ShipCity { get; set; }
    public string ShipCountry { get; set; }
    public string ShipPostalCode { get; set; }
    public IList<CreateOrderDetailViewModel> OrderDetails { get; set; } = new List<CreateOrderDetailViewModel>();

    public string InformationMessage { get; private set; }

    public IModelValidator<CreateOrderViewModel> Validator { get; set; }
    public ModelValidator<CreateOrderViewModel> ModelValidator { get; set; }

    public void AddNewOrderDetailItem()
    {
        OrderDetails.Add(new CreateOrderDetailViewModel());
    }

    public async Task Send()
    {
        InformationMessage = string.Empty;
        try
        {
            var orderId = await _gateway.CreateOrderAsync((CreateOrderDto)this);
            InformationMessage = string.Format(CreateOrderMessages.CreatedOrderTemplate, orderId);
        }
        catch (HttpRequestException ex)
        {
            if (ex.Data.Contains("Errors"))
            {
                IEnumerable<ValidationError> errors = ex.Data["errors"] as IEnumerable<ValidationError>;
                ModelValidator.AddErrors(errors);
            }
            else
            {
                throw;
            }
            
        }
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

