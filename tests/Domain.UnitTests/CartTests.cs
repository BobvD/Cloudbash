using Cloudbash.Domain.Carts;
using Cloudbash.Domain.Carts.Events;
using Cloudbash.Domain.UnitTests.Common;
using System;
using Xunit;

namespace Cloudbash.Domain.UnitTests
{
    public class CartTests : AggregateRootBaseTest<Cart>
    {
        private readonly Guid customerId = Guid.NewGuid();
        private readonly Guid ticketTypeId = Guid.NewGuid();

        [Fact]
        public void When_Create_New_Cart_CartCreatedEvent()
        {
            var cart = new Cart(customerId);

            AssertSingleUncommittedEvent<CartCreatedEvent>(cart, @event =>
            {
                Assert.Equal(customerId, @event.CustomerId);
                Assert.NotEqual(default(Guid), @event.AggregateId);
            });
        }

        [Fact]
        public void Given_Cart_When_Add_Item_Then_CartItemAddedEvent()
        {
            var cart = new Cart(customerId);
            ClearUncommittedEvents(cart);

            var item = new CartItem
            {
                TicketTypeId = ticketTypeId,
                Quantity = 2
            };

            cart.AddItem(item);

            AssertSingleUncommittedEvent<CartItemAddedEvent>(cart, @event =>
            {
                Assert.Equal(ticketTypeId, @event.Item.TicketTypeId);
                Assert.Equal(item.Quantity, @event.Item.Quantity);
                Assert.Equal(1, @event.AggregateVersion);
            });
        }


        [Fact]
        public void Given_Cart_When_Add_Item_Without_TicketTypeId_Then_Throws_Exception()
        {
            var cart = new Cart(customerId);
            ClearUncommittedEvents(cart);

            var item = new CartItem
            {
                Quantity = 2
            };

            Assert.Throws<ArgumentNullException>(() => { cart.AddItem(item); });
        }

        [Fact]
        public void Given_Cart_When_Add_Item_Quantity_Invalid_Then_Throws_Exception()
        {
            var cart = new Cart(customerId);
            ClearUncommittedEvents(cart);

            var item = new CartItem
            {
                TicketTypeId = ticketTypeId,
                Quantity = -1
            };

            Assert.Throws<ArgumentException>(() => { cart.AddItem(item); });
        }

        [Fact]
        public void Given_Cart_When_Remove_Item_Then_CartItemRemovedEvent()
        {
            var cart = new Cart(customerId);           

            var item = new CartItem
            {
                TicketTypeId = ticketTypeId,
                Quantity = 2
            };

            cart.AddItem(item);

            ClearUncommittedEvents(cart);

            cart.RemoveItem(item.Id);

            AssertSingleUncommittedEvent<CartItemRemovedEvent>(cart, @event =>
            {
                Assert.Equal(item.Id, @event.ItemId);
                Assert.Equal(2, @event.AggregateVersion);
            });

        }

    }
}
