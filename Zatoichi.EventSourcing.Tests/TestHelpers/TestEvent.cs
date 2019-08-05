namespace Zatoichi.EventSourcing.Tests.TestHelpers
{
    public class TestEvent : Event
    {
        public static TestEvent Instance => new TestEvent();

        public override void Apply(IEventEntity eventEntity)
        {
        }
    }
}