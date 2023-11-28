using Microsoft.Extensions.DependencyInjection;
using NorthWind.Sales.Backed.BusinessObjects.Exceptions;
using NorthWind.Sales.Backed.BusinessObjects.Interfaces.Common;
using NorthWind.Sales.Backed.BusinessObjects.Interfaces.CreateOrder;
using NorthWind.Sales.Backend.Presenters.ExceptionHandlers;

namespace NorthWind.Sales.Backend.Presenters.Extensions;

public static class DependencyContainer
{
    public static IServiceCollection AddPresenters(this IServiceCollection services)
    {
        services.AddScoped<ICreateOrderOutputPort, CreateOrderPresenter>();
        //services.AddSingleton<object, ValidationExceptionHandler>();
        //.NET8
        services.AddKeyedSingleton<object, ValidationExceptionHandler>(typeof(IExceptionHandler<>));
        //services.AddSingleton<object, UnitOfWorkExceptionHandler>();
        services.AddKeyedSingleton<object, UnitOfWorkExceptionHandler>(typeof(IExceptionHandler<>));

        services.AddSingleton<ExceptionHandlerOrchestrator>();
        return services;
    }
}
