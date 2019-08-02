namespace Zatoichi.EventSourcing
{
    using System;
    using System.Linq.Expressions;

    public abstract class Query<T>
        where T : EventEntity
    {
        public virtual Expression<Func<bool, T>> Where { get; set; }
    }
}