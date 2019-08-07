namespace Zatoichi.EventSourcing
{
    using System;

    public abstract class Event : IEvent
    {
        public DateTime CommitDate { get; set; }

        public virtual void Apply(ref IEntity entity) { }
    }
}