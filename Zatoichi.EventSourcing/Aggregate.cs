namespace Zatoichi.EventSourcing
{
    using System.Collections.Generic;
    using System.Linq;
    using ChaosMonkey.Guards;
    using Common.Infrastructure.Extensions;
    using MediatR;
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
        protected Queue<INotification> domainEvents;
        protected int version = -1; // TODO if this were a real library, I'd write a version service for aggregates.

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

        public virtual void AddEvents(ICollection<INotification> events)
        {
            Guard.IsNotNull(events, nameof(events));
            events.Each(this.domainEvents.Enqueue);
        }

        protected virtual void AddDomainEvent(INotification eventItem)
        {
            // lazy load
            this.domainEvents = this.domainEvents ?? new Queue<INotification>();
            this.domainEvents.Enqueue(eventItem);
        }

        [JsonProperty]
        public IAggregateId RootId { get; protected set; }

        public IReadOnlyCollection<INotification> DomainEvents => this.domainEvents?.ToList().AsReadOnly();

        [JsonProperty]
        public virtual int Version => this.version;
    }
}