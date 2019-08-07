namespace Zatoichi.EventSourcing.Tests.TestHelpers
{
    public class TestDomainEvent : DomainEvent
    {
        public static TestDomainEvent Instance => new TestDomainEvent("v1");

        public TestDomainEvent(string version)
            : base(version)
        {
        }

        public override string Body { get; protected set; }
        public override string EventType { get; protected set; }
    }
}