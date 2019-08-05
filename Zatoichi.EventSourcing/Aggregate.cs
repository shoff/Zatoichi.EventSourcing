namespace Zatoichi.EventSourcing
{
    using System.Collections.Generic;
    using System.Threading;
    using ChaosMonkey.Guards;
    using Common.Infrastructure.Extensions;

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
        protected readonly Queue<Event> pendingEvents = new Queue<Event>();
        protected int version = -1; // TODO if this were a real library, I'd write a version service for aggregates.

        protected Aggregate() { }

        protected Aggregate(
            IAggregateId rootId)
        {
            this.RootId = rootId;
        }

        public virtual void ClearPendingEvents()
        {
            this.pendingEvents.Clear();
        }

        public virtual void ApplyEvents()
        {
            while (this.pendingEvents.TryDequeue(out var @event))
            {
                @event.Apply(this);
            }
        }
        /// <summary>
        /// Adds the event to the aggregate and increments the version.
        /// The version incrementation is thread-safe
        /// </summary>
        /// <param name="event"></param>
        public virtual void RaiseEvent(Event @event)
        {
            Guard.IsNotNull(@event, nameof(@event));
            // TODO hmm not too sure how I want to do this
            Interlocked.Increment(ref this.version);
            @event.Revision = this.version;
        }
        public virtual void AddEvents(ICollection<Event> events)
        {
            Guard.IsNotNull(events, nameof(events));
            events.Each(this.pendingEvents.Enqueue);
        }
        public IAggregateId RootId { get; protected set; }
        public virtual int Version => this.version;
        public int PendingEventCount => this.pendingEvents.Count;
    }
}