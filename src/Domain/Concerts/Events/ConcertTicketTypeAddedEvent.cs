using Cloudbash.Domain.SeedWork;
using System;

namespace Cloudbash.Domain.Concerts.Events
{
    public class ConcertTicketTypeAddedEvent : DomainEventBase
    {
        public TicketType Type { get; set; }

        public ConcertTicketTypeAddedEvent() { }

        internal ConcertTicketTypeAddedEvent(Guid aggregateId, TicketType type) 
            : base(aggregateId) {
            Type = type;
        }

        internal ConcertTicketTypeAddedEvent(Guid aggregateId, long aggregateVersion, TicketType type) 
            : base(aggregateId, aggregateVersion) {
            Type = type;
        }
        
        public override IDomainEvent WithAggregate(Guid aggregateId, long aggregateVersion)
        {
            return new ConcertTicketTypeAddedEvent(aggregateId, aggregateVersion, Type);
        }
    }
}
