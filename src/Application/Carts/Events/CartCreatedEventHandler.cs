using Cloudbash.Application.Common.Events;
using Cloudbash.Application.Common.Interfaces;
using Cloudbash.Domain.Carts.Events;
using Cloudbash.Domain.ReadModels;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Cloudbash.Application.Carts.Events
{
    public class CartCreatedEventHandler : INotificationHandler<DomainEventNotification<CartCreatedEvent>>
    {
        private readonly IViewModelRepository<Cart> _cartRepository;

        public CartCreatedEventHandler(IViewModelRepository<Cart> cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task Handle(DomainEventNotification<CartCreatedEvent> notification, CancellationToken cancellationToken)
        {
            var @event = notification.DomainEvent;

            await _cartRepository.AddAsync(
                    new Cart { 
                        Id = @event.AggregateId.ToString(), 
                        CustomerId = @event.CustomerId 
                    });
        }
    }
}
