using Cloudbash.Application.Common.Events;
using Cloudbash.Application.Common.Interfaces;
using Cloudbash.Domain.ViewModels;
using Cloudbash.Domain.Concerts.Events;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Cloudbash.Application.Concerts.Events
{
    public class ConcertCreatedEventHandler : INotificationHandler<DomainEventNotification<ConcertCreatedEvent>>
    {
        private readonly IViewModelRepository<Concert> _concertRepository;

        public ConcertCreatedEventHandler(IViewModelRepository<Concert> concertRepository)
        {
            _concertRepository = concertRepository;
        }

        public async Task Handle(DomainEventNotification<ConcertCreatedEvent> notification, CancellationToken cancellationToken)
        {
            var @event = notification.DomainEvent;
            await _concertRepository.AddAsync(
                new Concert { Id = @event.AggregateId, Name = @event.Name, Venue = @event.Venue, ImageUrl = @event.ImageUrl, Date = @event.Date });            
        }
    }
}
