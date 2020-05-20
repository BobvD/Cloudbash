using Cloudbash.Domain.SeedWork;
using System;

namespace Cloudbash.Domain.Concerts.Events
{
    public class ConcertTicketTypeRemoved : DomainEventBase
    {
        public Guid TicketTypeId { get; set; }

        public ConcertTicketTypeRemoved() { }

        internal ConcertTicketTypeRemoved(Guid aggregateId, Guid ticketTypeId)
            : base(aggregateId)
        {
            TicketTypeId = ticketTypeId;
        }

        internal ConcertTicketTypeRemoved(Guid aggregateId, long aggregateVersion, Guid ticketTypeId)
            : base(aggregateId, aggregateVersion)
        {
            TicketTypeId = ticketTypeId;
        }

        public override IDomainEvent WithAggregate(Guid aggregateId, long aggregateVersion)
        {
            return new ConcertTicketTypeRemoved(aggregateId, aggregateVersion, TicketTypeId);
        }
    }
}

