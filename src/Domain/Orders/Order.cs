using Cloudbash.Domain.Carts;
using Cloudbash.Domain.Orders.Events;
using Cloudbash.Domain.SeedWork;
using System;
using System.Collections.Generic;

namespace Cloudbash.Domain.Orders
{
    public class Order : AggregateRootBase
    {
        private List<OrderItem> Items;    
        public Guid CustomerId { get; protected set; }
        
        public static Order Create(Cart cart)
        {
            List<OrderItem> items = new List<OrderItem>();
            foreach (CartItem cartItem in cart.Items)
            {
                items.Add(new OrderItem { TicketId = cartItem.TicketTypeId });
            }

            return new Order(cart.CustomerId, items);
        }

        private Order(Guid customerId, List<OrderItem> items)
        {
            Id = Guid.NewGuid();
            AddEvent(new OrderCreatedEvent(Id, customerId, items));
        }

        internal void Apply(OrderCreatedEvent @event)
        {
            Id = @event.AggregateId;
            CustomerId = @event.CustomerId;
            Items = @event.Items;
        }

    }
}
