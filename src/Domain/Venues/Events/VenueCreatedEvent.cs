using Cloudbash.Domain.SeedWork;
using System;

namespace Cloudbash.Domain.Venues.Events
{
    public class VenueCreatedEvent : DomainEventBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Capacity { get; set; }
        public string WebUrl { get; set; }
        public string Address { get; set; }

        public VenueCreatedEvent() { }

        public VenueCreatedEvent(Guid aggregateId, string name, string description, 
            int capacity, string webUrl, string address) : base(aggregateId)
        {
            Name = name;
            Description = description;
            Capacity = capacity;
            WebUrl = webUrl;
            Address = address;
        }

        public VenueCreatedEvent(Guid aggregateId, long aggregateVersion, string name, 
            string description, int capacity, string webUrl, string address) : base(aggregateId, aggregateVersion)
        {
            Name = name;
            Description = description;
            Capacity = capacity;
            WebUrl = webUrl;
            Address = address;
        }


        public override IDomainEvent WithAggregate(Guid aggregateId, long aggregateVersion)
        {
            return new VenueCreatedEvent(aggregateId, aggregateVersion, Name, Description, Capacity, WebUrl, Address);
        }
    }
}
