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
        void StoreEvent<T>(Event<T> @event);
        Task StoreEventAsync<T>(Event<T> @event);
        ICollection<Event<T>> Where<T>(Expression<Predicate<T>> predicate);
    }
}