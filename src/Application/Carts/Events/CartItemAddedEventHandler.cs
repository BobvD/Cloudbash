
using Cloudbash.Application.Common.Events;
using Cloudbash.Application.Common.Interfaces;
using Cloudbash.Domain.Carts.Events;
using Cloudbash.Domain.ReadModels;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Cloudbash.Application.Carts.Events
{
    public class CartItemAddedEventHandler : INotificationHandler<DomainEventNotification<CartItemAddedEvent>>
    {
        private readonly IViewModelRepository<Cart> _cartRepository;

        public CartItemAddedEventHandler(IViewModelRepository<Cart> cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task Handle(DomainEventNotification<CartItemAddedEvent> notification, CancellationToken cancellationToken)
        {
            var @event = notification.DomainEvent;
            var item = @event.Item;

            var children = new [] { "Items" };
            var cart =  await _cartRepository.GetAsync(@event.AggregateId, children);

            cart.Items.Add(
                new CartItem { 
                    Id = item.Id.ToString(), 
                    Quantity = item.Quantity, TicketTypeId = 
                    item.TicketTypeId.ToString() 
                });

            await _cartRepository.UpdateAsync(cart);
            
        }
    }
}
