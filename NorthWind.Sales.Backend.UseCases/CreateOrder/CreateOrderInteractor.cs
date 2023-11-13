using NorthWind.Sales.Backed.BusinessObjects.Aggregates;
using NorthWind.Sales.Backed.BusinessObjects.Interfaces.CreateOrder;
using NorthWind.Sales.Backed.BusinessObjects.Interfaces.Repositories;
using NorthWind.Sales.Entities.Dtos;
// ReSharper disable IdentifierTypo

namespace NorthWind.Sales.Backend.UseCases.CreateOrder;

internal class CreateOrderInteractor : ICreateOrderInputPort
{
    private readonly ICreateOrderOutputPort _outputPort;
    private readonly ICommandsRepository _repository;

    public CreateOrderInteractor(ICreateOrderOutputPort outputPort, ICommandsRepository repository)
    {
        _outputPort = outputPort;
        _repository = repository;
    }

    public async ValueTask Handle(CreateOrderDto order)
    {
        var orderAggregate = OrderAggregate.FromDto(order);
        await _repository.CreateOrder(orderAggregate);
        await _repository.SaveChanges();
        await _outputPort.Handle(orderAggregate);
    }
}
