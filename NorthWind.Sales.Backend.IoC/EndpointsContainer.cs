using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using NorthWind.Sales.Backend.Controllers;

namespace NorthWind.Sales.Backend.IoC;

public static class EndpointsContainer
{
    public static WebApplication MapNorthWindSalesEndpoints(this WebApplication app)
    {
        app.UseCreateOrderController();
        return app;
    }
}
