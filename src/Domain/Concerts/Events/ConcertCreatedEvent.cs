using Cloudbash.Domain.SeedWork;
using System;

namespace Cloudbash.Domain.Concerts.Events
{
    public class ConcertCreatedEvent : DomainEventBase
    {
        public ConcertCreatedEvent()
        {
        }

        internal ConcertCreatedEvent(Guid aggregateId, string name, Guid venueId, string imageUrl, string date) 
            : base(aggregateId)
        {
            Name = name;
            VenueId = venueId;
            ImageUrl = imageUrl;
            Date = date;
        }

        internal ConcertCreatedEvent(Guid aggregateId, long aggregateVersion, string name, Guid venueId, string imageUrl, string date) 
            : base(aggregateId, aggregateVersion)
        {
            Name = name;
            VenueId = venueId;
            ImageUrl = imageUrl;
            Date = date;
        }
               
        public string Name { get; private set; }
        public Guid VenueId { get; set; }
        public string ImageUrl { get; set; }
        public string Date { get; set; }

        public override IDomainEvent WithAggregate(Guid aggregateId, long aggregateVersion)
        {
            return new ConcertCreatedEvent(aggregateId, aggregateVersion, Name, VenueId, ImageUrl, Date);
        }

        public override string ToString()
        {
            return Name + base.ToString();
        }
    }
}
