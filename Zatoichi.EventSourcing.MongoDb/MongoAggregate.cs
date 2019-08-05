namespace Zatoichi.EventSourcing.MongoDb
{
    public abstract class MongoAggregate : Aggregate
    {
        protected MongoAggregate(IAggregateId rootId) 
            : base(rootId)
        {
        }
    }
}