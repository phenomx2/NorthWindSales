using Microsoft.Extensions.DependencyInjection;
using NorthWind.Sales.Backed.BusinessObjects.Interfaces.CreateOrder;

namespace NorthWind.Sales.Backend.Presenters.Extensions;

public static class DependencyContainer
{
    public static IServiceCollection AddPresenters(this IServiceCollection services)
    {
        services.AddScoped<ICreateOrderOutputPort, CreateOrderPresenter>();
        return services;
    }
}
