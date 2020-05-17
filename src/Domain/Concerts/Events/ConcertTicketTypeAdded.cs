using Cloudbash.Domain.SeedWork;
using System;

namespace Cloudbash.Domain.Concerts.Events
{
    public class ConcertTicketTypeAdded : DomainEventBase
    {
        public TicketType Type { get; set; }

        public ConcertTicketTypeAdded() { }

        internal ConcertTicketTypeAdded(Guid aggregateId, TicketType type) 
            : base(aggregateId) {
            Type = type;
        }

        internal ConcertTicketTypeAdded(Guid aggregateId, long aggregateVersion, TicketType type) 
            : base(aggregateId, aggregateVersion) {
            Type = type;
        }
        
        public override IDomainEvent WithAggregate(Guid aggregateId, long aggregateVersion)
        {
            return new ConcertTicketTypeAdded(aggregateId, aggregateVersion, Type);
        }
    }
}
