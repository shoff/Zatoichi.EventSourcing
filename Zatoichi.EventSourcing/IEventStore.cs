namespace Zatoichi.EventSourcing
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public interface IEventStore
    {
        // For now "snapshot" will also hold a collection events that have not been applied
        T GetLatestSnapShot<T>();
        void StoreEvent<T>(Event @event);
        Task StoreEventAsync<T>(Event @event);
        ICollection<Event> Where<T>(Expression<Func<Event, bool>> predicate);
        Task<ICollection<Event>> GetEventStream<TAggregate>(Guid id) where TAggregate : IAggregate, new();
    }
}