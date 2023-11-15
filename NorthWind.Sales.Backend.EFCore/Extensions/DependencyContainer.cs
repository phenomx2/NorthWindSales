using Microsoft.Extensions.DependencyInjection;
using NorthWind.Sales.Backed.BusinessObjects.Interfaces.Repositories;
using NorthWind.Sales.Backend.EFCore.DataContexts;
using NorthWind.Sales.Backend.EFCore.Options;
using NorthWind.Sales.Backend.EFCore.Repositories;

namespace NorthWind.Sales.Backend.EFCore.Extensions;

public static class DependencyContainer
{
    public static IServiceCollection AddRepositories(this IServiceCollection services, Action<DbOptions> configureDbOptions)
    {
        services.Configure(configureDbOptions);
        //por default se registra como scoped 
        services.AddDbContext<NorthWindSalesContext>();
        services.AddScoped<ICommandsRepository, CommandsRepository>();
        return services;
    }
}
