using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cloudbash.Domain.SeedWork
{
    public abstract class Aggregate : IAggregate { 

        public long Version { get; protected set; }
        public Guid Id { get; protected set; }

        private readonly ICollection<IDomainEvent> _uncommittedEvents = new LinkedList<IDomainEvent>();

        public void ApplyEvent(IDomainEvent @event, long version)
        {
            if (!_uncommittedEvents.Any(x => Equals(x.EventId, @event.EventId)))
            {
                ((dynamic)this).Apply((dynamic)@event);
                Version = version;
            }
        }

        public void ClearUncommittedEvents()
        {
            _uncommittedEvents.Clear();
        }

        public IEnumerable<IDomainEvent> GetUncommittedEvents()
        {
            return _uncommittedEvents.AsEnumerable();
        }

            protected void RaiseEvent<TEvent>(TEvent @event)
            where TEvent : DomainEvent
        {
            IDomainEvent eventWithAggregate = @event.WithAggregate(@event.AggregateId, 1);
            ((IAggregate)this).ApplyEvent(eventWithAggregate, 1);
            _uncommittedEvents.Add(eventWithAggregate);
        }

    }
}
