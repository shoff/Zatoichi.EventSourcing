namespace Zatoichi.EventSourcing
{
    using System;
    using Newtonsoft.Json;

    public class IntegrationEventId : IIntegrationEventId
    {
        public IntegrationEventId(Guid id)
        {
            this.Id = id;
        }

        [JsonProperty]
        public Guid Id { get; private set; }
    }
}