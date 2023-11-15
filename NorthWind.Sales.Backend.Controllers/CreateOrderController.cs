using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using NorthWind.Sales.Backed.BusinessObjects.Interfaces.CreateOrder;
using NorthWind.Sales.Entities.Dtos;
using NorthWind.Sales.Entities.ValueObjects;

namespace NorthWind.Sales.Backend.Controllers;

public static class CreateOrderController
{
    public static WebApplication UseCreateOrderController(this WebApplication app)
    {
        app.MapPost(Endpoints.CreateOrder, async (
            CreateOrderDto orderDto,
            ICreateOrderInputPort inputPort,
            ICreateOrderOutputPort presenter) => 
            Results.Ok(await CreateOrder(orderDto, inputPort, presenter)));
        return app;
    }

    private static async Task<int> CreateOrder(
        CreateOrderDto orderDto,
        ICreateOrderInputPort inputPort, 
        ICreateOrderOutputPort presenter)
    {
        await inputPort.Handle(orderDto);
        return presenter.OrderId;
    }
}
