namespace Zatoichi.EventSourcing.MongoDb
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Config;
    using Microsoft.Extensions.Options;
    using MongoDB.Driver;

    public class MongoEventStore : IEventStore
    {
        private IMongoDatabase database;
        private const string ADMIN = "admin";

        public MongoEventStore(
            IOptions<MongoOptions> options)
        {
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

        public T GetLatestSnapShot<T>()
        {
            throw new NotImplementedException();
        }

        public ICollection<Event<T>> Where<T>(Expression<Predicate<T>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}