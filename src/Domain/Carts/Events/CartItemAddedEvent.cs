using Cloudbash.Domain.SeedWork;
using System;
namespace Cloudbash.Domain.Carts.Events
{
    public class CartItemAddedEvent : DomainEventBase
    {
        public CartItemAddedEvent() { }

        internal CartItemAddedEvent(Guid aggregateId, CartItem item)
            : base(aggregateId)
        {
            Item = item;
        }

        internal CartItemAddedEvent(Guid aggregateId, long aggregateVersion, CartItem item)
            : base(aggregateId, aggregateVersion)
        {
            Item = item;
        }

        public CartItem Item { get; private set; }

        public override IDomainEvent WithAggregate(Guid aggregateId, long aggregateVersion)
        {
            return new CartItemAddedEvent(aggregateId, aggregateVersion, Item);
        }
    }
}
