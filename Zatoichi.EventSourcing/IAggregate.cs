namespace Zatoichi.EventSourcing
{
    public interface IAggregate : IEntity
    {
        IAggregateId RootId { get; }
        int Version { get; }
        void ClearPendingEvents();
    }
}