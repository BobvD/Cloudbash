using Cloudbash.Domain.SeedWork;
using System;

namespace Cloudbash.Domain.Concerts.Events
{
    public class ConcertTicketTypeRemovedEvent : DomainEventBase
    {       
        public ConcertTicketTypeRemovedEvent() { }

        internal ConcertTicketTypeRemovedEvent(Guid aggregateId, Guid ticketTypeId)
            : base(aggregateId)
        {
            TicketTypeId = ticketTypeId;
        }

        internal ConcertTicketTypeRemovedEvent(Guid aggregateId, long aggregateVersion, Guid ticketTypeId)
            : base(aggregateId, aggregateVersion)
        {
            TicketTypeId = ticketTypeId;
        }

        public Guid TicketTypeId { get; private set; }

        public override IDomainEvent WithAggregate(Guid aggregateId, long aggregateVersion)
        {
            return new ConcertTicketTypeRemovedEvent(aggregateId, aggregateVersion, TicketTypeId);
        }
    }
}

