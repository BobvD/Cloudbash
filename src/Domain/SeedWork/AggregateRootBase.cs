using System;
using System.Collections.Generic;
using System.Linq;

namespace Cloudbash.Domain.SeedWork
{
    public abstract class AggregateRootBase : IAggregateRoot
    {
        public const long NewAggregateVersion = -1;
        private long _version = NewAggregateVersion;
        long IAggregateRoot.Version => _version;

        public Guid Id { get; protected set; }
        public DateTime Created { get; protected set; }

        private readonly ICollection<IDomainEvent> _uncommittedEvents = new LinkedList<IDomainEvent>();


        protected void AddEvent<TEvent>(TEvent @event) where TEvent : DomainEventBase
        {
            IDomainEvent eventWithAggregate = @event.WithAggregate(
                Equals(Id, default(Guid)) ? @event.AggregateId : Id, ++_version);

            ApplyEvent(eventWithAggregate, _version);

            _uncommittedEvents.Add(eventWithAggregate);
        }

        public void ApplyEvent(IDomainEvent @event, long version)
        {
            if (!_uncommittedEvents.Any(x => Equals(x.EventId, @event.EventId)))
            {
                ((dynamic)this).Apply((dynamic)@event);
                _version = version;
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
