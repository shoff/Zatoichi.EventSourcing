namespace Zatoichi.EventSourcing
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public interface IEventStore
    {
        // For now "snapshot" will also hold a collection events that have not been applied
        T GetLatestSnapShot<T>();
        ICollection<Event<T>> Where<T>(Expression<Predicate<T>> predicate);
    }
}