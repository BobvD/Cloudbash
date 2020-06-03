using Cloudbash.Domain.SeedWork;
using System;

namespace Cloudbash.Domain.Venues.Events
{
    public class VenueCreatedEvent : DomainEventBase
    {
       

        public VenueCreatedEvent() { }

        internal VenueCreatedEvent(Guid aggregateId, string name, string description, 
            int capacity, string webUrl, string address) : base(aggregateId)
        {
            Name = name;
            Description = description;
            Capacity = capacity;
            WebUrl = webUrl;
            Address = address;
        }

        internal VenueCreatedEvent(Guid aggregateId, long aggregateVersion, string name, 
            string description, int capacity, string webUrl, string address) : base(aggregateId, aggregateVersion)
        {
            Name = name;
            Description = description;
            Capacity = capacity;
            WebUrl = webUrl;
            Address = address;
        }

        public string Name { get; private set; }
        public string Description { get; private set; }
        public int Capacity { get; private set; }
        public string WebUrl { get; private set; }
        public string Address { get; private set; }
        
        public override IDomainEvent WithAggregate(Guid aggregateId, long aggregateVersion)
        {
            return new VenueCreatedEvent(aggregateId, aggregateVersion, Name, Description, Capacity, WebUrl, Address);
        }
    }
}
