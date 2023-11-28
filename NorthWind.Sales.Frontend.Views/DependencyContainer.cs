using Microsoft.Extensions.DependencyInjection;
using NorthWind.Sales.Entities.Interfaces.Common;
using NorthWind.Sales.Frontend.Views.ViewModels;

namespace NorthWind.Sales.Frontend.Views;

public static class DependencyContainer
{
    public static IServiceCollection AddViewServices(this IServiceCollection services)
    {
        services.AddScoped<CreateOrderViewModel>();
        services.AddScoped<IModelValidator<CreateOrderViewModel>, CreateOrderViewModelValidator>();
        return services;
    }
}
