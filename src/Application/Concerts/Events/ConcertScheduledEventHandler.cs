using Cloudbash.Application.Common.Events;
using Cloudbash.Application.Common.Interfaces;
using Cloudbash.Domain.Concerts.Events;
using Cloudbash.Domain.ViewModels;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Cloudbash.Application.Concerts.Events
{
    public class ConcertScheduledEventHandler : INotificationHandler<DomainEventNotification<ConcertScheduledEvent>>
    {
        private readonly IViewModelRepository<Concert> _concertRepository;

        public ConcertScheduledEventHandler(IViewModelRepository<Concert> concertRepository)
        {
            _concertRepository = concertRepository;
        }

        public async Task Handle(DomainEventNotification<ConcertScheduledEvent> notification, CancellationToken cancellationToken)
        {
            var @event = notification.DomainEvent;

            var concert = await _concertRepository.GetAsync(@event.AggregateId);

            concert.StartDate = @event.StartDate;
            concert.EndDate = @event.EndDate;

            await _concertRepository.UpdateAsync(concert);
        }
    }
}
