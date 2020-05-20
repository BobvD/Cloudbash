using Cloudbash.Domain.SeedWork;
using System;

namespace Cloudbash.Domain.Concerts.Events
{
    public class ConcertPublishedEvent : DomainEventBase
    {
        public ConcertPublishedEvent() { }

        internal ConcertPublishedEvent(Guid aggregateId) : base(aggregateId) { }

        internal ConcertPublishedEvent(Guid aggregateId, long aggregateVersion) : base(aggregateId, aggregateVersion) { }

        public override IDomainEvent WithAggregate(Guid aggregateId, long aggregateVersion)
        {
            return new ConcertDeletedEvent(aggregateId, aggregateVersion);
        }
    }
}
