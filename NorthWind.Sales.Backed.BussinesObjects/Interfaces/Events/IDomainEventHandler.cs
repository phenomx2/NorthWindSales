namespace NorthWind.Sales.Backed.BusinessObjects.Interfaces.Events;

public interface IDomainEventHandler<TEventType> where TEventType : IDomainEvent
{
    ValueTask Handle(TEventType eventTypeInstance);
}
