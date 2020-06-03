using Cloudbash.Domain.Carts;
using Cloudbash.Domain.Orders;
using Cloudbash.Domain.Orders.Events;
using Cloudbash.Domain.UnitTests.Common;
using System;
using Xunit;

namespace Cloudbash.Domain.UnitTests
{
    public class OrderTests : AggregateRootBaseTest<Order>
    {
        private readonly Guid customerId = Guid.NewGuid();
        private readonly Guid ticketTypeId = Guid.NewGuid();

        [Fact]
        public void When_Create_New_Order_OrderCreatedEvent()
        {
            var cart = new Cart(customerId);

            var item = new CartItem
            {
                TicketTypeId = ticketTypeId,
                Quantity = 2
            };

            cart.AddItem(item);

            var order = Order.Create(cart);

            AssertSingleUncommittedEvent<OrderCreatedEvent>(order, @event =>
            {
                Assert.Equal(customerId, @event.CustomerId);
                Assert.NotEqual(default(Guid), @event.AggregateId);
            });
        }
    }
}
