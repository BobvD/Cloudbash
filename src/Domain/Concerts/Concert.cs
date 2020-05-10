using Cloudbash.Domain.Concerts.Events;
using Cloudbash.Domain.SeedWork;
using System;

namespace Cloudbash.Domain.Concerts
{
    public class Concert : AggregateRootBase
    {
        private Concert() { }

        public Concert(string name, Guid venueId, string imageUrl, string date)
        {
            
            Id = Guid.NewGuid();
            // Create and add a new ConcertCreatedEvent
            AddEvent(new ConcertCreatedEvent(Id, name, venueId, imageUrl, date));
        }        

        public string Name { get; set; }
        public Guid VenueId { get; set; }
        public string ImageUrl { get; set; }
        public string Date { get; set; }
        public bool IsDeleted { get; set; }

        public void MarkAsDeleted()
        {
            if(!IsDeleted)
                AddEvent(new ConcertDeletedEvent(Id));
        }

        internal void Apply(ConcertCreatedEvent @event)
        {
            Id = @event.AggregateId;
            Name = @event.Name;
            VenueId = @event.VenueId;
            ImageUrl = @event.ImageUrl;
            Date = @event.Date;
        }

        internal void Apply(ConcertDeletedEvent @event)
        {
            IsDeleted = true;
        }

        public override string ToString()
        {
            return base.Id + " - " + Name + " - ";
        }
    }
}
