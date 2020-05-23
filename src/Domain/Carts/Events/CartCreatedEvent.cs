using Cloudbash.Domain.SeedWork;
using System;

namespace Cloudbash.Domain.Carts.Events
{
    public class CartCreatedEvent : DomainEventBase
    {
        public CartCreatedEvent() { }

        internal CartCreatedEvent(Guid aggregateId, Guid customerId) 
            : base(aggregateId) {
            CustomerId = customerId;
        }

        internal CartCreatedEvent(Guid aggregateId, long aggregateVersion, Guid customerId) 
            : base(aggregateId, aggregateVersion) {
            CustomerId = customerId;
        }

        public Guid CustomerId { get; set; }

        public override IDomainEvent WithAggregate(Guid aggregateId, long aggregateVersion)
        {
            return new CartCreatedEvent(aggregateId, aggregateVersion, CustomerId);
        }
    }
}
