namespace Zatoichi.EventSourcing
{
    using ChaosMonkey.Guards;
    using Newtonsoft.Json;

    public class EventVersion
    {
        public EventVersion(string id)
        {
            Guard.IsNotNullOrWhitespace(id, nameof(id));
            this.Id = id;
        }

        [JsonProperty]
        public string Id { get; private set; }
    }
}