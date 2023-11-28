using NorthWind.Sales.Backed.BusinessObjects.Aggregates;
using NorthWind.Sales.Backed.BusinessObjects.Exceptions;
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
    private readonly IEnumerable<IModelValidator<CreateOrderDto>> _validators;

    public CreateOrderInteractor(ICreateOrderOutputPort outputPort,
        ICommandsRepository repository,
        IEnumerable<IModelValidator<CreateOrderDto>> validators)
    {
        _outputPort = outputPort;
        _repository = repository;
        _validators = validators;
    }

    public async ValueTask Handle(CreateOrderDto order)
    {
        using var enumerator = _validators.GetEnumerator();
        bool isValid = true;
        while (isValid && enumerator.MoveNext())
        {
            isValid = await enumerator.Current.Validate(order);
            if (!isValid)
            {
                throw new ValidationException(enumerator.Current.Errors);
            }
        }
        
        var orderAggregate = OrderAggregate.FromDto(order);

        await _repository.CreateOrder(orderAggregate);
        await _repository.SaveChanges();
        await _outputPort.Handle(orderAggregate);
    }
}
