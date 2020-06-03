using Cloudbash.Domain.SeedWork;
using System;

namespace Cloudbash.Domain.Carts.Events
{
    public class CartCheckedOutEvent : DomainEventBase
    {
        public CartCheckedOutEvent() { }

        internal CartCheckedOutEvent(Guid aggregateId)
            : base(aggregateId)
        {
        }

        internal CartCheckedOutEvent(Guid aggregateId, long aggregateVersion)
            : base(aggregateId, aggregateVersion)
        {
        }


        public override IDomainEvent WithAggregate(Guid aggregateId, long aggregateVersion)
        {
            return new CartCheckedOutEvent(aggregateId, aggregateVersion);
        }
    }
}

