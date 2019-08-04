namespace Zatoichi.EventSourcing
{
    using System;
    using System.Linq.Expressions;

    public abstract class Query<T>
    {
        public virtual Expression<Func<bool, T>> Where { get; set; }
    }
}