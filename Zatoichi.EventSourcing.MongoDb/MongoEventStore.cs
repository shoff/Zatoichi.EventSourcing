namespace Zatoichi.EventSourcing.MongoDb
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
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

        public virtual void StoreEvent<T>(DomainEvent domainEvent)
        {
            this.logger.LogDebug($"Add:{typeof(T).Name} {domainEvent.ToJson()}");
            this.database.GetCollection<DomainEvent>(typeof(DomainEvent).Name).InsertOne(domainEvent);
        }

        public virtual Task StoreEventAsync<T>(DomainEvent domainEvent)
        {
            this.logger.LogDebug($"Add:{typeof(T).Name} {domainEvent.ToJson()}");
            return this.database.GetCollection<DomainEvent>(typeof(DomainEvent).Name).InsertOneAsync(domainEvent);
        }

        public virtual ICollection<DomainEvent> Where<T>(Expression<Func<DomainEvent, bool>> predicate)
        {
            var collection = this.database.GetCollection<DomainEvent>(typeof(T).Name);
            var events = collection.AsQueryable().Where(predicate.Compile()).ToList();
            return events;
        }

        public virtual Task<ICollection<DomainEvent>> GetEventStream<TAggregate>(Guid id) where TAggregate : IAggregate, new()
        {
            var collection = this.database.GetCollection<DomainEvent>(typeof(TAggregate).Name);
            var events = collection.AsQueryable().ToList();
            return Task.FromResult((ICollection<DomainEvent>)events);
        }
    }
}