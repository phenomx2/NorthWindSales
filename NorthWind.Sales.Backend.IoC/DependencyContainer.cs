using Microsoft.Extensions.DependencyInjection;
using NorthWind.Sales.Backend.EFCore.Extensions;
using NorthWind.Sales.Backend.EFCore.Options;
using NorthWind.Sales.Backend.Presenters.Extensions;
using NorthWind.Sales.Backend.UseCases.Extensions;

namespace NorthWind.Sales.Backend.IoC;

public static class DependencyContainer
{
    public static IServiceCollection AddNorthWindSalesServices(this IServiceCollection services,
        Action<DbOptions> configureDbOptions)
    {
        services.AddUseCasesServices()
            .AddRepositories(configureDbOptions)
            .AddPresenters();
        return services;
    }
}