namespace Zatoichi.EventSourcing.Tests.TestHelpers
{
    public class TestAggregate : Aggregate
    {
        public TestAggregate(IAggregateId id = null)
        {
            this.RootId = id ?? new AggregateId();
        }
    }
}