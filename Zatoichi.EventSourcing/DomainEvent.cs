namespace Zatoichi.EventSourcing
{
    using System;
    using Newtonsoft.Json;

    public abstract class DomainEvent : EventArgs, IDomainEvent
    {
        protected DomainEvent(string version)
        {
            this.Version = new EventVersion(version);
            this.CommitDate = DateTime.UtcNow;
        }

        [JsonProperty]
        public string Body { get; protected set; }

        [JsonProperty]
        public string EventType { get; protected set; }

        [JsonProperty]
        public EventVersion Version { get; private set; }

        [JsonProperty]
        public virtual DateTime CommitDate { get; private set; }

    }
}