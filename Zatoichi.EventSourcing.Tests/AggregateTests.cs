namespace Zatoichi.EventSourcing.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoFixture;
    using Common.Infrastructure.Extensions;
    using Common.UnitTest;
    using TestHelpers;
    using Xunit;

    public class AggregateTests : BaseTest
    {
        private readonly Aggregate aggregate;

        public AggregateTests()
        {
            this.aggregate = new TestAggregate();
        }

        [Fact]
        public void RaiseEvent_Throws_If_Event_Is_Null()
        {
            Assert.Throws<ArgumentNullException>(() => this.aggregate.RaiseEvent(null));
        }

        [Fact]
        public void RaiseEvent_Causes_Version_To_Increment()
        {
            var version = this.aggregate.Version;
            var @event = TestEvent.Instance;
            this.aggregate.RaiseEvent(@event);
            Assert.Equal(version + 1, this.aggregate.Version);
        }

        [Fact]
        public void AddEvents_Throws_If_Events_Is_Null()
        {
            Assert.Throws<ArgumentNullException>(() => this.aggregate.AddEvents(null));
        }

        [Fact]
        public void ApplyEvents_Consumes_The_Queue()
        {
            var events = this.fixture.Create<List<TestEvent>>();
            this.aggregate.AddEvents(events.Map(e => (Event)e).ToList());
            var pendingCount = this.aggregate.PendingEventCount;
            this.aggregate.ApplyEvents();

            Assert.Equal(events.Count, pendingCount);
            Assert.Equal(0, this.aggregate.PendingEventCount);
        }

    }
}