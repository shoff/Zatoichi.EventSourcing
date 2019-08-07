namespace Zatoichi.EventSourcing
{
    using System;
    using Newtonsoft.Json;

    public class AggregateId : IAggregateId
    {
        [JsonConstructor]
        public AggregateId()
        { }

        public AggregateId(Guid? id)
        {
            this.RootId = id ?? new Guid();
        }

        [JsonProperty]
        public Guid RootId { get; protected set; } = Guid.Empty;
    }
}