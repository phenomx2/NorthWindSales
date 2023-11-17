using Microsoft.Extensions.DependencyInjection;
using NorthWind.Sales.Frontend.Views.ViewModels;

namespace NorthWind.Sales.Frontend.Views;

public static class DependencyContainer
{
    public static IServiceCollection AddViewServices(this IServiceCollection services)
    {
        services.AddScoped<CreateOrderViewModel>();
        return services;
    }
}
