using Microsoft.Extensions.DependencyInjection;
using NorthWind.Sales.Entities.Validators;
using NorthWind.Sales.Frontend.Views;
using NorthWind.Sales.Frontend.WebApiGateways;

namespace NorthWind.Sales.Frontend.IoC;

public static class DependencyContainer
{
    public static IServiceCollection AddNorthWindSalesServices(this IServiceCollection services,
        Action<HttpClient> configureHttpClient)
    {
        services.AddWebApiGateways(configureHttpClient)
            .AddViewServices()
            .AddValidators();
        return services;
    }
}
