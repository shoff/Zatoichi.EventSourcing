namespace Zatoichi.EventSourcing.Tests.TestHelpers
{
    public class TestEvent : Event
    {
        public static TestEvent Instance => new TestEvent();

        public override T Apply<T>(T t)
        {
            return t;
        }
    }
}