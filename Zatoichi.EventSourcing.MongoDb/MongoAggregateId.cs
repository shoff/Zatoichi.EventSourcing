namespace Zatoichi.EventSourcing.MongoDb
{
    using System;
    using MongoDB.Bson.Serialization.Attributes;

    public class MongoAggregateId : IAggregateId
    {
        public MongoAggregateId(Guid? id)
        {
            this.RootId = id ?? new Guid();
        }

        [BsonId]
        public Guid RootId { get; set; }
    }
}