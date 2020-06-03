using Cloudbash.Application.Common.Events;
using Cloudbash.Application.Common.Interfaces;
using Cloudbash.Domain.Carts.Events;
using Cloudbash.Domain.ReadModels;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Cloudbash.Application.Carts.Events
{
    class CheckOutCartEventHandler : INotificationHandler<DomainEventNotification<CartCheckedOutEvent>>
    {
        private readonly IViewModelRepository<Cart> _cartRepository;

        public CheckOutCartEventHandler(IViewModelRepository<Cart> cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task Handle(DomainEventNotification<CartCheckedOutEvent> notification, CancellationToken cancellationToken)
        {
            var @event = notification.DomainEvent;   
            var children = new[] { "Items" };
            var cart = await _cartRepository.GetAsync(@event.AggregateId, children);

            cart.Items = null;

            await _cartRepository.UpdateAsync(cart);
            await _cartRepository.DeleteAsync(@event.AggregateId);

        }
    }
}
