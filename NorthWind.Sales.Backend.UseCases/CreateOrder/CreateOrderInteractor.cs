using NorthWind.Sales.Backed.BusinessObjects.Aggregates;
using NorthWind.Sales.Backed.BusinessObjects.Interfaces.CreateOrder;
using NorthWind.Sales.Backed.BusinessObjects.Interfaces.Repositories;
using NorthWind.Sales.Entities.Dtos;
using NorthWind.Sales.Entities.Interfaces.Common;

// ReSharper disable IdentifierTypo

namespace NorthWind.Sales.Backend.UseCases.CreateOrder;

internal class CreateOrderInteractor : ICreateOrderInputPort
{
    private readonly ICreateOrderOutputPort _outputPort;
    private readonly ICommandsRepository _repository;
    private readonly IModelValidator<CreateOrderDto> _validator;

    public CreateOrderInteractor(ICreateOrderOutputPort outputPort,
        ICommandsRepository repository,
        IModelValidator<CreateOrderDto> validator)
    {
        _outputPort = outputPort;
        _repository = repository;
        _validator = validator;
    }

    public async ValueTask Handle(CreateOrderDto order)
    {
        if (!await _validator.Validate(order))
        {
            var errors = string.Join(" ", _validator.Errors.Select(e => $"{e.PropertyName}: {e.Message}"));
            throw new Exception(errors);
        }
        var orderAggregate = OrderAggregate.FromDto(order);

        await _repository.CreateOrder(orderAggregate);
        await _repository.SaveChanges();
        await _outputPort.Handle(orderAggregate);
    }
}
