using Cloudbash.Domain.SeedWork;
using Cloudbash.Domain.Venues.Events;
using System;

namespace Cloudbash.Domain.Venues
{
    public class Venue : AggregateRootBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Capacity { get; set; }
        public string WebUrl { get; set; }
        public string Address { get; set; }
        public bool IsDeleted { get; set; }

        public Venue() { }

        public Venue(string name, string description, int capacity, string webUrl, string address)
        {
            Id = Guid.NewGuid();
            AddEvent(new VenueCreatedEvent(Id, name, description, capacity, webUrl, address));
        }

        public void MarkAsDeleted()
        {
            if (!IsDeleted)
            {
                AddEvent(new VenueDeletedEvent(Id));
            }
        }


        internal void Apply(VenueCreatedEvent @event)
        {
            Id = @event.AggregateId;
            Name = @event.Name;
            Description = @event.Description;
            Capacity = @event.Capacity;
            WebUrl = @event.WebUrl;
            Address = @event.Address;
        }

        internal void Apply(VenueDeletedEvent @event)
        {
            IsDeleted = true;
        }

        public override string ToString()
        {
            return base.Id + " - " + Name;
        }

    }
}
