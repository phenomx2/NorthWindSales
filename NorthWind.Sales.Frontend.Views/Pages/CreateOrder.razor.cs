using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using NorthWind.Sales.Frontend.Views.ViewModels;

namespace NorthWind.Sales.Frontend.Views.Pages;

public partial class CreateOrder
{
    [Inject]
    private CreateOrderViewModel ViewModel { get; set; }

#pragma warning disable CS0649 // Field is never assigned to, and will always have its default value
    private ErrorBoundary _errorBoundaryRef;
#pragma warning restore CS0649 // Field is never assigned to, and will always have its default value

    private void Recover()
    {
        _errorBoundaryRef?.Recover();
    }
}