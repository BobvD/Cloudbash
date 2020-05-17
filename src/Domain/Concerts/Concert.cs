using Cloudbash.Domain.Concerts.Events;
using Cloudbash.Domain.SeedWork;
using System;
using System.Collections.Generic;

namespace Cloudbash.Domain.Concerts
{
    public class Concert : AggregateRootBase
    {
        private Concert() {
            TicketTypes = new List<TicketType>();
        }
        
        public string Name { get; set; }
        public Guid VenueId { get; set; }
        public string ImageUrl { get; set; }
        public string Date { get; set; }
        public ConcertStatus Status { get; set; }
        private List<TicketType> TicketTypes { get; set; }

        public Concert(string name, Guid venueId, string imageUrl) : this()
        {
            Id = Guid.NewGuid();
            Created = DateTime.Now;
            // Create and add a new ConcertCreatedEvent
            AddEvent(new ConcertCreatedEvent(Id, name, venueId, imageUrl, Created));
        }

        public void MarkAsDeleted()
        {
            if(!Status.Equals(ConcertStatus.DELETED))
                AddEvent(new ConcertDeletedEvent(Id));
        }

        public void AddTicketType(TicketType type)
        {
            AddEvent(new ConcertTicketTypeAdded(Id, type));
        }

        internal void Apply(ConcertCreatedEvent @event)
        {
            Id = @event.AggregateId;
            Name = @event.Name;
            VenueId = @event.VenueId;
            ImageUrl = @event.ImageUrl;
            Created = @event.Created;
            Status = ConcertStatus.DRAFT;           
        }

        internal void Apply(ConcertDeletedEvent @event)
        {
            Status = ConcertStatus.DELETED;
        }

        internal void Apply(ConcertTicketTypeAdded @event)
        {
            TicketTypes.Add(@event.Type);
        }

        public override string ToString()
        {
            return base.Id + " - " + Name + " - ";
        }
    }
}
