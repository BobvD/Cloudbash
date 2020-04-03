using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Cloudbash.Domain.SeedWork
{
    public abstract class AggregateRootBase : IAggregateRoot
    {
        public long Version { get; protected set; }

        public Guid Id { get; protected set; }

        private readonly ICollection<IDomainEvent> _uncommittedEvents = new List<IDomainEvent>();

        public void AddEvent(IDomainEvent @event)
        {
            _uncommittedEvents.Add(@event);
            ApplyEvent(@event);
        }

        public void ApplyEvent(IDomainEvent @event)
        {
            if (!_uncommittedEvents.Any(x => Equals(x.EventId, @event.EventId)))
            {
                ((dynamic)this).Apply((dynamic)@event);
                Version++;
            }
        }

        public IEnumerable<IDomainEvent> GetUncommittedEvents()
        {
            return _uncommittedEvents.AsEnumerable();
        }

        public void ClearUncommittedEvents()
        {
            _uncommittedEvents.Clear();
        }

    }
}
