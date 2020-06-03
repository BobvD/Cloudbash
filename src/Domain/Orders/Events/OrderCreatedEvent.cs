
using Cloudbash.Domain.SeedWork;
using System;
using System.Collections.Generic;

namespace Cloudbash.Domain.Orders.Events
{
    public class OrderCreatedEvent : DomainEventBase
    {
        public OrderCreatedEvent() { }

        internal OrderCreatedEvent(Guid aggregateId, Guid customerId, List<OrderItem> items) : base(aggregateId) {
            CustomerId = customerId;
            Items = items;
        }

        internal OrderCreatedEvent(Guid aggregateId, long aggregateVersion, Guid customerId, List<OrderItem> items) : base(aggregateId, aggregateVersion) {
            CustomerId = customerId;
            Items = items;
        }

        public Guid CustomerId { get; private set; }
        public List<OrderItem> Items { get; private set; }

        public override IDomainEvent WithAggregate(Guid aggregateId, long aggregateVersion)
        {
            return new OrderCreatedEvent(aggregateId, aggregateVersion, CustomerId, Items);
        }
    }
}
