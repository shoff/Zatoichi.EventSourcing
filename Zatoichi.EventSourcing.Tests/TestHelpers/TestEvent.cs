namespace Zatoichi.EventSourcing.Tests.TestHelpers
{
    public class TestEvent : Event
    {
        public static TestEvent Instance => new TestEvent();
    }
}