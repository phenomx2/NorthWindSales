using Microsoft.Extensions.DependencyInjection;
using NorthWind.Sales.Entities.Dtos;
using NorthWind.Sales.Entities.Interfaces.Common;
using NorthWind.Sales.Entities.Validators.CreateOrder;

namespace NorthWind.Sales.Entities.Validators;

public static class DependencyContainer
{
    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddScoped<IModelValidator<CreateOrderDto>, CreateOrderDtoValidator>();
        return services;
    }
}