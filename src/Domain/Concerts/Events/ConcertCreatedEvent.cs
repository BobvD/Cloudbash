using Cloudbash.Domain.SeedWork;
using System;

namespace Cloudbash.Domain.Concerts.Events
{
    public class ConcertCreatedEvent : DomainEventBase
    {
        public ConcertCreatedEvent() {}

        internal ConcertCreatedEvent(Guid aggregateId, string name, Guid venueId, string imageUrl, DateTime created) 
            : base(aggregateId)
        {
            Name = name;
            VenueId = venueId;
            ImageUrl = imageUrl;
            Created = created;
        }

        internal ConcertCreatedEvent(Guid aggregateId, long aggregateVersion, string name, Guid venueId, string imageUrl, DateTime created) 
            : base(aggregateId, aggregateVersion)
        {
            Name = name;
            VenueId = venueId;
            ImageUrl = imageUrl;
            Created = created;
        }
               
        public string Name { get; private set; }
        public Guid VenueId { get; private set; }
        public string ImageUrl { get; private set; }
        public DateTime Created { get; private set; }

        public override IDomainEvent WithAggregate(Guid aggregateId, long aggregateVersion)
        {
            return new ConcertCreatedEvent(aggregateId, aggregateVersion, Name, VenueId, ImageUrl, Created);
        }

    }
}
