using Cloudbash.Application.Common.Events;
using Cloudbash.Application.Common.Interfaces;
using Cloudbash.Domain.ReadModels;
using Cloudbash.Domain.Carts.Events;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace Cloudbash.Application.Carts.Events
{
    public class CartItemRemovedEventHandler : INotificationHandler<DomainEventNotification<CartItemRemovedEvent>>
    {
        private readonly IViewModelRepository<Cart> _cartRepository;

        public CartItemRemovedEventHandler(IViewModelRepository<Cart> cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task Handle(DomainEventNotification<CartItemRemovedEvent> notification, CancellationToken cancellationToken)
        {
            var @event = notification.DomainEvent;

            var children = new string[] { "Items" };
            var cart = await _cartRepository.GetAsync(@event.AggregateId, children);         

            var cartItem = cart.Items
                .SingleOrDefault(t => t.Id == @event.ItemId.ToString());

            if (cartItem != null)
            {
                cart.Items.Remove(cartItem);
            }

            await _cartRepository.UpdateAsync(cart);

        }
    }
}
