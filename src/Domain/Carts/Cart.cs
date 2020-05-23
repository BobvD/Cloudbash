using Cloudbash.Domain.Carts.Events;
using Cloudbash.Domain.SeedWork;
using System;
using System.Collections.Generic;

namespace Cloudbash.Domain.Carts
{
    public class Cart : AggregateRootBase
    {
        public Guid CustomerId { get; set; }
        public List<CartItem> Items { get; set; }

        public Cart(Guid customerId)
        {
            Id = Guid.NewGuid();
            AddEvent(new CartCreatedEvent(Id, customerId));
        }

        public void AddItem()
        {

        }

        public void RemoveItem()
        {

        }

        public void CheckOut()
        {

        }

        internal void Apply(CartCreatedEvent @event)
        {
            Id = @event.AggregateId;
            CustomerId = @event.CustomerId;
            Items = new List<CartItem>();
        }
    }
}
