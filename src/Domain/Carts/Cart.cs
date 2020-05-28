using Cloudbash.Domain.Carts.Events;
using Cloudbash.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cloudbash.Domain.Carts
{
    public class Cart : AggregateRootBase
    {
        public Cart() { 
            Items = new List<CartItem>(); 
        }

        public Guid CustomerId { get; set; }
        public List<CartItem> Items { get; set; }

        public Cart(Guid customerId)
        {           
            if (customerId == null)
            {
                throw new ArgumentNullException(nameof(customerId));
            }

            Id = Guid.NewGuid();
            AddEvent(new CartCreatedEvent(Id, customerId));
        }

        public void AddItem(CartItem item)
        {
            if (item.TicketTypeId == null)
            {
                throw new ArgumentNullException(nameof(item.TicketTypeId));
            }

            CheckQuantity(item.Quantity);

            AddEvent(new CartItemAddedEvent(Id, item));

        }

        public void RemoveItem()
        {

        }

        public void CheckOut()
        {

        }

        private bool ContainsTicketType(Guid ticketTypeId)
        {
            return Items.Any(x => x.TicketTypeId == ticketTypeId);
        }

        private static void CheckQuantity(int quantity)
        {
            if (quantity <= 0)
            {
                throw new ArgumentException("Quantity must be greater than zero", nameof(quantity));
            }
        }

        internal void Apply(CartCreatedEvent @event)
        {
            Id = @event.AggregateId;
            CustomerId = @event.CustomerId;
            Items = new List<CartItem>();
        }

        internal void Apply(CartItemAddedEvent @event)
        {
            Items.Add(@event.Item);
        }
    }
}
