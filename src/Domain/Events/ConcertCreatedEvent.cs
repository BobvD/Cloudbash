using Cloudbash.Domain.SeedWork;
using System;

namespace Cloudbash.Domain.Events
{
    public class ConcertCreatedEvent : DomainEventBase
    {
        public ConcertCreatedEvent()
        {
        }

        internal ConcertCreatedEvent(Guid aggregateId, string name, string venue, string imageUrl, string date) : base(aggregateId)
        {
            Name = name;
            Venue = venue;
            ImageUrl = imageUrl;
            Date = date;
        }
        
        public string Name { get; private set; }
        public string Venue { get; set; }
        public string ImageUrl { get; set; }
        public string Date { get; set; }

        public override IDomainEvent WithAggregate(Guid aggregateId, long aggregateVersion)
        {
            return new ConcertCreatedEvent(aggregateId, Name, Venue, ImageUrl, Date);
        }

        public override string ToString()
        {
            return Name + base.ToString();
        }
    }
}
