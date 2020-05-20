using Cloudbash.Domain.Concerts.Events;
using Cloudbash.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;

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
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ConcertStatus Status { get; set; }
        private List<TicketType> TicketTypes { get; set; }

        public Concert(string name, Guid venueId, string imageUrl) : this()
        {
            Id = Guid.NewGuid();
            Created = DateTime.Now;
            AddEvent(new ConcertCreatedEvent(Id, name, venueId, imageUrl, Created));
        }

        public void Publish()
        {
            if (Status.Equals(ConcertStatus.DRAFT))
                AddEvent(new ConcertPublishedEvent(Id));
        }

        public void MarkAsDeleted()
        {
            if(!Status.Equals(ConcertStatus.DELETED))
                AddEvent(new ConcertDeletedEvent(Id));
        }

        public void AddTicketType(TicketType type)
        {
            AddEvent(new ConcertTicketTypeAddedEvent(Id, type));
        }

        public void RemoveTicketType(Guid ticketTypeId)
        {
            if(TicketTypes.Any(t => t.Id.Equals(ticketTypeId)))
                AddEvent(new ConcertTicketTypeRemovedEvent(Id, ticketTypeId));    
        }

        public void Schedule(DateTime start, DateTime end)
        {
            AddEvent(new ConcertScheduledEvent(Id, start, end));
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


        internal void Apply(ConcertPublishedEvent @event)
        {
            Status = ConcertStatus.PUBLISHED;
        }

        internal void Apply(ConcertTicketTypeAddedEvent @event)
        {
            TicketTypes.Add(@event.Type);
        }

        internal void Apply(ConcertTicketTypeRemovedEvent @event)
        {
            var ticketType = TicketTypes
                .SingleOrDefault(t => t.Id == @event.TicketTypeId);
            if(ticketType != null)
            {
                TicketTypes.Remove(ticketType);
            }
        }

        internal void Apply(ConcertScheduledEvent @event)
        {
            StartDate = @event.StartDate;
            EndDate = @event.EndDate;
        }

        public override string ToString()
        {
            return base.Id + " - " + Name + " - ";
        }
    }
}
