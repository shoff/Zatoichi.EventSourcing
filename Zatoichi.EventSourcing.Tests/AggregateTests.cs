namespace Zatoichi.EventSourcing.Tests
{
    using System;
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
        public void AddEvents_Throws_If_Events_Is_Null()
        {
            Assert.Throws<ArgumentNullException>(() => this.aggregate.AddEvents(null));
        }



    }
}