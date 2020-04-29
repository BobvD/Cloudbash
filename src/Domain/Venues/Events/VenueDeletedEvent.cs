using Cloudbash.Domain.SeedWork;
using System;

namespace Cloudbash.Domain.Venues.Events
{
    public class VenueDeletedEvent : DomainEventBase
    {
        internal VenueDeletedEvent() { }

        internal VenueDeletedEvent(Guid aggregateId) : base(aggregateId) { }

        internal VenueDeletedEvent(Guid aggregateId, long aggregateVersion) : base(aggregateId, aggregateVersion) { }

        public override IDomainEvent WithAggregate(Guid aggregateId, long aggregateVersion)
        {
            return new VenueDeletedEvent(aggregateId, aggregateVersion);
        }
    }
}
