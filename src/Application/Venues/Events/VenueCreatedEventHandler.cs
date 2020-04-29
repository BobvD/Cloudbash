
using Cloudbash.Application.Common.Events;
using Cloudbash.Application.Common.Interfaces;
using Cloudbash.Domain.Venues.Events;
using Cloudbash.Domain.ViewModels;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Cloudbash.Application.Venues.Events
{
    class VenueCreatedEventHandler : INotificationHandler<DomainEventNotification<VenueCreatedEvent>>
    {
        private readonly IViewModelRepository<Venue> _repository;

        public VenueCreatedEventHandler(IViewModelRepository<Venue> repository)
        {
            _repository = repository;
        }

        public async Task Handle(DomainEventNotification<VenueCreatedEvent> notification, CancellationToken cancellationToken)
        {
            var @event = notification.DomainEvent;
            await _repository.AddAsync(
                    new Venue { Id = @event.AggregateId, 
                                Name = @event.Name, 
                                Description = @event.Description, 
                                Capacity = @event.Capacity, 
                                WebUrl = @event.WebUrl,
                                Address = @event.Address });
        }
    }
}