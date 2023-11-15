using Microsoft.Extensions.DependencyInjection;
using NorthWind.Sales.Backed.BusinessObjects.Interfaces.CreateOrder;
using NorthWind.Sales.Backend.UseCases.CreateOrder;

namespace NorthWind.Sales.Backend.UseCases.Extensions;

public static class DependencyContainer
{
    public static IServiceCollection AddUseCasesServices(this IServiceCollection services)
    {
        services.AddScoped<ICreateOrderInputPort, CreateOrderInteractor>();
        return services;
    }
}
