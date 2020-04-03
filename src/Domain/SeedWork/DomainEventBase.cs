using System;
using System.Collections.Generic;

namespace Cloudbash.Domain.SeedWork
{
    public abstract class DomainEventBase : IDomainEvent, IEquatable<DomainEventBase>
    {
        public Guid EventId { get; private set; }

        public Guid AggregateId { get; private set; }

        public long AggregateVersion { get; private set; }

        protected DomainEventBase()
        {
            EventId = Guid.NewGuid();
        }

        protected DomainEventBase(Guid aggregateId) : this()
        {
            AggregateId = aggregateId;
        }

        protected DomainEventBase(Guid aggregateId, long aggregateVersion) : this(aggregateId)
        {
            AggregateVersion = aggregateVersion;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj as DomainEventBase);
        }

        public bool Equals(DomainEventBase other)
        {
            return other != null &&
                   EventId.Equals(other.EventId);
        }

        public override int GetHashCode()
        {
            return 19031992 + EqualityComparer<Guid>.Default.GetHashCode(EventId);
        }

        public abstract IDomainEvent WithAggregate(Guid aggregateId, long aggregateVersion);
    }
}
