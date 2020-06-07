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
    public class CartCreatedEventHandlerTests : EventHandlerTestBase, IDisposable
    {
        public CartCreatedEventHandlerTests(TestFixture fixture) : base(fixture)
        {
        }

        public void Dispose()
        {
           
        }

        [Fact]
        public async Task When_Cart_Created_Event_Handled_ConcertVm_Saved()
        {
            var cart = new Cart(Guid.NewGuid());

            CartCreatedEvent @event = (CartCreatedEvent)cart.GetUncommittedEvents().Last();

            var handler = new CartCreatedEventHandler(_cartRepo);

            var notification = new DomainEventNotification<CartCreatedEvent>(@event);

            await handler.Handle(notification, CancellationToken.None);

            var carts = await _cartRepo.GetAllAsync();

            carts.Count.ShouldBe(2);

        }

    }
}