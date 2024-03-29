﻿namespace Zatoichi.EventSourcing
{
    using System.Collections.Generic;
    using System.Linq;
    using ChaosMonkey.Guards;
    using Common.Infrastructure.Extensions;
    using Newtonsoft.Json;

    /// <summary>
    /// </summary>
    /// <remarks>
    /// c. 1400, from Latin aggregatus "associated, united," past participle of aggregare
    /// "add to (a flock), lead to a flock, bring together (in a flock)," figuratively
    /// "attach, join, include; collect, bring together," from ad "to" (see ad-) + gregare
    /// "to collect into a flock, gather," from grex (genitive gregis) "a flock,
    /// "from PIE root *ger- "to gather."
    /// </remarks>
    public abstract class Aggregate : IAggregate
    {
        protected Queue<DomainEvent> domainEvents;
        protected int version = 1;

        protected Aggregate() { }

        protected Aggregate(IAggregateId rootId)
        {
            this.RootId = rootId;
            
        }

        public virtual void ClearPendingEvents()
        {
            this.domainEvents.Clear();
        }

        public virtual void ApplyEvents() {}

        public virtual void AddEvents(ICollection<DomainEvent> events)
        {
            Guard.IsNotNull(events, nameof(events));
            events.Each(this.domainEvents.Enqueue);
        }

        protected virtual void AddDomainEvent(DomainEvent eventItem)
        {
            // lazy load
            this.domainEvents = this.domainEvents ?? new Queue<DomainEvent>();
            this.domainEvents.Enqueue(eventItem);
        }

        [JsonProperty]
        public IAggregateId RootId { get; protected set; }

        public IReadOnlyCollection<DomainEvent> DomainEvents => this.domainEvents?.ToList().AsReadOnly();

        [JsonProperty]
        public virtual int Version
        {
            get => this.version;
            protected set => this.version = value;
        }
    }
}