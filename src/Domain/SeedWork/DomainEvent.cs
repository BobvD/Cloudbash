using System;
using System.Collections.Generic;
using System.Text;

namespace Cloudbash.Domain.SeedWork
{
    public abstract class DomainEvent : IDomainEvent, IEquatable<DomainEvent>
    {
        protected DomainEvent()
        {
            EventId = Guid.NewGuid();
        }

        protected DomainEvent(Guid aggregateId) : this()
        {
            AggregateId = aggregateId;
        }

        protected DomainEvent(Guid aggregateId, long aggregateVersion) : this(aggregateId)
        {
            AggregateVersion = aggregateVersion;
        }

        public Guid EventId { get; private set; }

        public Guid AggregateId { get; private set; }

        public long AggregateVersion { get; private set; }

        public override bool Equals(object obj)
        {
            return base.Equals(obj as DomainEvent);
        }

        public bool Equals(DomainEvent other)
        {
            return other != null &&
                   EventId.Equals(other.EventId);
        }

        public override int GetHashCode()
        {
            return 000 + EqualityComparer<Guid>.Default.GetHashCode(EventId);
        }

        public abstract IDomainEvent WithAggregate(Guid aggregateId, long aggregateVersion);

    }
}
