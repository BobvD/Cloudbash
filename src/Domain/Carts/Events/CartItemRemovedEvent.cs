using Cloudbash.Domain.SeedWork;
using System;

namespace Cloudbash.Domain.Carts.Events
{
    public class CartItemRemovedEvent : DomainEventBase
    {
        public CartItemRemovedEvent() { }

        internal CartItemRemovedEvent(Guid aggregateId, Guid itemId)
            : base(aggregateId)
        {
            ItemId = itemId;
        }

        internal CartItemRemovedEvent(Guid aggregateId, long aggregateVersion, Guid itemId)
            : base(aggregateId, aggregateVersion)
        {
            ItemId = itemId;
        }

        public Guid ItemId { get; private set; }

        public override IDomainEvent WithAggregate(Guid aggregateId, long aggregateVersion)
        {
            return new CartItemRemovedEvent(aggregateId, aggregateVersion, ItemId);
        }
    }
}
