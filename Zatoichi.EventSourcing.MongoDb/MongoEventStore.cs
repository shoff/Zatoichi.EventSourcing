namespace Zatoichi.EventSourcing.MongoDb
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Config;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using MongoDB.Bson;
    using MongoDB.Driver;

    public class MongoEventStore : IEventStore
    {
        private readonly ILogger<MongoEventStore> logger;
        private readonly IMongoDatabase database;
        private const string ADMIN = "admin";

        public MongoEventStore(
            IOptions<MongoOptions> options,
            ILogger<MongoEventStore> logger)
        {
            this.logger = logger;
            MongoInternalIdentity internalIdentity = new MongoInternalIdentity(ADMIN, options.Value.Username);
            PasswordEvidence passwordEvidence = new PasswordEvidence(options.Value.Password);
            MongoCredential mongoCredential = new MongoCredential(options.Value.AuthMechanism, internalIdentity, passwordEvidence);

            MongoClientSettings settings = new MongoClientSettings
            {
                Credential = mongoCredential,
                Server = new MongoServerAddress(options.Value.MongoHost, int.Parse(options.Value.Port))
            };

            var client = new MongoClient(settings);
            this.database = client.GetDatabase(options.Value.DefaultDb);
        }
        
        public virtual T GetLatestSnapShot<T>()
        {
            var collection = this.database.GetCollection<T>(typeof(T).Name);
            return collection.AsQueryable().Last();
        }

        public virtual void StoreEvent<T>(Event @event)
        {
            this.logger.LogDebug($"Add:{typeof(T).Name} {@event.ToJson()}");
            this.database.GetCollection<Event>(typeof(Event).Name).InsertOne(@event);
        }

        public virtual Task StoreEventAsync<T>(Event @event)
        {
            this.logger.LogDebug($"Add:{typeof(T).Name} {@event.ToJson()}");
            return this.database.GetCollection<Event>(typeof(Event).Name).InsertOneAsync(@event);
        }

        public virtual ICollection<Event> Where<T>(Expression<Func<Event, bool>> predicate)
        {
            var collection = this.database.GetCollection<Event>(typeof(T).Name);
            var events = collection.AsQueryable().Where(predicate.Compile()).ToList();
            return events;
        }

        public virtual Task<ICollection<Event>> GetEventStream<TAggregate>(Guid id) where TAggregate : IAggregate, new()
        {
            var collection = this.database.GetCollection<Event>(typeof(TAggregate).Name);
            var events = collection.AsQueryable().ToList();
            return Task.FromResult((ICollection<Event>)events);
        }
    }
}