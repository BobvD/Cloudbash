using Cloudbash.Domain.SeedWork;
using System;
namespace Cloudbash.Domain.Carts.Events
{
    public class CartItemAddedEvent : DomainEventBase
    {
        public CartItemAddedEvent() { }

        internal CartItemAddedEvent(Guid aggregateId, Guid ticketTypeId, int quantity)
            : base(aggregateId)
        {
            TicketTypeId = ticketTypeId;
            Quantity = quantity;
        }

        internal CartItemAddedEvent(Guid aggregateId, long aggregateVersion, Guid ticketTypeId, int quantity)
            : base(aggregateId, aggregateVersion)
        {
            TicketTypeId = ticketTypeId;
            Quantity = quantity;
        }

        public Guid TicketTypeId { get; set; }
        public int Quantity { get; set; }

        public override IDomainEvent WithAggregate(Guid aggregateId, long aggregateVersion)
        {
            return new CartItemAddedEvent(aggregateId, aggregateVersion, TicketTypeId, Quantity);
        }
    }
}
