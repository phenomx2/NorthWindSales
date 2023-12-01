namespace NorthWind.Sales.Backed.BusinessObjects.Interfaces.Events;

public interface IDomainEventHub<TEventType> where TEventType : IDomainEvent
{
    ValueTask Raise(TEventType eventTypeInstance);
}