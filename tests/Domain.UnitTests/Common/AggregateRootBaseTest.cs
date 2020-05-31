using Cloudbash.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Cloudbash.Domain.UnitTests.Common
{
    public abstract class AggregateRootBaseTest<TAggregate>
        where TAggregate : AggregateRootBase, IAggregateRoot
    {
        protected void AssertSingleUncommittedEventOfType<TEvent>(TAggregate aggregate)
            where TEvent : IDomainEvent
        {
            var uncommittedEvents = GetUncommittedEventsOf(aggregate);

            Assert.Single(uncommittedEvents);
            Assert.IsType<TEvent>(uncommittedEvents.First());
        }

        protected void AssertSingleUncommittedEvent<TEvent>(TAggregate aggregate, Action<TEvent> assertions)
            where TEvent : IDomainEvent
        {
            AssertSingleUncommittedEventOfType<TEvent>(aggregate);
            assertions((TEvent)((IAggregateRoot)aggregate).GetUncommittedEvents().Single());
        }

        protected void ClearUncommittedEvents(TAggregate aggregate)
        {
            ((IAggregateRoot)aggregate).ClearUncommittedEvents();
        }

        protected IEnumerable<IDomainEvent> GetUncommittedEventsOf(TAggregate aggregate)
        {
            return ((IAggregateRoot)aggregate).GetUncommittedEvents();
        }
    }
}
