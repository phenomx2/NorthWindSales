using NorthWind.Sales.Backed.BusinessObjects.Interfaces.Events;

namespace NorthWind.Sales.Backed.BusinessObjects.Services;

public class DomainEventHub<TEventType> : IDomainEventHub<TEventType> where TEventType : IDomainEvent
{
    private readonly IEnumerable<IDomainEventHandler<TEventType>> _domainEventHandlers;

    public DomainEventHub(IEnumerable<IDomainEventHandler<TEventType>> domainEventHandlers)
    {
        _domainEventHandlers = domainEventHandlers;
    }

    public async ValueTask Raise(TEventType eventTypeInstance)
    {
        foreach (var domainEventHandler in _domainEventHandlers)
        {
            await domainEventHandler.Handle(eventTypeInstance);
        }
    }
}
