namespace Zatoichi.EventSourcing
{
    public interface IAggregate
    {
        IAggregateId RootId { get; }
        int Version { get; }
        void RaiseEvent(Event @event);
        void ClearPendingEvents();
        void ApplyEvents();
    }
}