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
        void StoreEvent<T>(DomainEvent domainEvent);
        Task StoreEventAsync<T>(DomainEvent domainEvent);
        ICollection<DomainEvent> Where<T>(Expression<Func<DomainEvent, bool>> predicate);
        Task<ICollection<DomainEvent>> GetEventStream<TAggregate>(Guid id) where TAggregate : IAggregate, new();
    }
}