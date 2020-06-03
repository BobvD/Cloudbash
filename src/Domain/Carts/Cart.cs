using Cloudbash.Domain.Carts.Events;
using Cloudbash.Domain.Carts.Exceptions;
using Cloudbash.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cloudbash.Domain.Carts
{
    public class Cart : AggregateRootBase
    {
        public Cart()
        {
            Items = new List<CartItem>();
        }

        public Guid CustomerId { get; set; }
        public List<CartItem> Items { get; set; }
        public bool IsCheckedOut { get; set; }

        public Cart(Guid customerId)
        {
            Id = Guid.NewGuid();
            AddEvent(new CartCreatedEvent(Id, customerId));
        }

        public void AddItem(CartItem item)
        {
            if (item.TicketTypeId == default(Guid))
            {
                throw new ArgumentNullException(nameof(item.TicketTypeId));
            }

            CheckQuantity(item.Quantity);

            AddEvent(new CartItemAddedEvent(Id, item));
        }

        public void RemoveItem(Guid cartItemId)
        {
            if (cartItemId == default(Guid))
            {
                throw new ArgumentException(nameof(CartItem));
            }

            if (Items.Any(t => t.Id.Equals(cartItemId)))
            {
                AddEvent(new CartItemRemovedEvent(Id, cartItemId));
            } 
        }


        public void CheckOut()
        {
            if (this.Items.Count == 0)
            {
                throw new EmptyCartCheckOutException();
            }
            AddEvent(new CartCheckedOutEvent(Id));            
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

        internal void Apply(CartCheckedOutEvent @event)
        {
            this.IsCheckedOut = true;
        }

        internal void Apply(CartItemRemovedEvent @event)
        {
            var item = Items
                .SingleOrDefault(t => t.Id == @event.ItemId);

            if (item != null)
            {
                Items.Remove(item);
            }
        }

        private static void CheckQuantity(int quantity)
        {
            if (quantity <= 0)
            {
                throw new ArgumentException("Quantity must be greater than zero", nameof(quantity));
            }
        }




    }
}
