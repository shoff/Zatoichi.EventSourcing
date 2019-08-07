namespace Zatoichi.EventSourcing
{
    public interface IAggregate : IEventEntity
    {
        IAggregateId RootId { get; }
        int Version { get; }
        void ClearPendingEvents();
    }
}