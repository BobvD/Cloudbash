using Cloudbash.Domain.Events;
using Cloudbash.Domain.SeedWork;
using System;

namespace Cloudbash.Domain.Entities
{
    public class Concert : AggregateRootBase
    {
        private Concert() { }

        public Concert(string name, string venue, string imageUrl, string date)
        {
            
            Id = Guid.NewGuid();
            // Create and add a new ConcertCreatedEvent
            AddEvent(new ConcertCreatedEvent(Id, name, venue, imageUrl, date));
        }        

        public string Name { get; set; }
        public string Venue { get; set; }
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
            Venue = @event.Venue;
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
