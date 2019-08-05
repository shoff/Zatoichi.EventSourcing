namespace Zatoichi.EventSourcing
{
    using System;

    public class AggregateId : IAggregateId
    {
        public AggregateId(Guid? id = null)
        {
            this.RootId = id ?? new Guid();
        }

        public Guid RootId { get; protected set; }
    }
}