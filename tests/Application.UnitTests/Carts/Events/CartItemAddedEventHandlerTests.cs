using Cloudbash.Application.Carts.Events;
using Cloudbash.Application.Common.Events;
using Cloudbash.Domain.Carts;
using Cloudbash.Domain.Carts.Events;
using Shouldly;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Cloudbash.Application.UnitTests.Carts.Events
{
    [Collection("EventHandlerTests")]
    public class CartItemAddedEventHandlerTests : EventHandlerTestBase
    {
        private Guid _ticketTypeId = new Guid("4f356bc0-ebe0-409a-aea7-d7fdd7b76154");

        public CartItemAddedEventHandlerTests(TestFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public async Task When_Concert_Created_Event_Handled_ConcertVm_Saved()
        {
            var cart = await CreateAndSaveCartAggregate();

            cart.ClearUncommittedEvents();

            var item = new CartItem { 
                Quantity = 2, 
                TicketTypeId = _ticketTypeId
            };

            cart.AddItem(item);

            CartItemAddedEvent @event = (CartItemAddedEvent)cart.GetUncommittedEvents().Last();

            var handler = new CartItemAddedEventHandler(_cartRepo);

            var notification = new DomainEventNotification<CartItemAddedEvent>(@event);

            await handler.Handle(notification, CancellationToken.None);

            var updatedCart = await _cartRepo.GetAsync(cart.Id, new[] { "Items" } );

            updatedCart.Items.Count.ShouldBe(1);
            var itemFromCart = updatedCart.Items.First();
            itemFromCart.Quantity.ShouldBe(2);
            itemFromCart.TicketType.Id.ShouldBe(_ticketTypeId.ToString());
        }

    }
}