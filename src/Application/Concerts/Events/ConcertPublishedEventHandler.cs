using Cloudbash.Application.Common.Events;
using Cloudbash.Application.Common.Interfaces;
using Cloudbash.Domain.Concerts.Events;
using Cloudbash.Domain.ReadModels;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Cloudbash.Application.Concerts.Events
{
    public class ConcertPublishedEventHandler : INotificationHandler<DomainEventNotification<ConcertPublishedEvent>>
    {
        private readonly IViewModelRepository<Concert> _concertRepository;

        public ConcertPublishedEventHandler(IViewModelRepository<Concert> concertRepository)
        {
            _concertRepository = concertRepository;
        }

        public async Task Handle(DomainEventNotification<ConcertPublishedEvent> notification, CancellationToken cancellationToken)
        {
            var @event = notification.DomainEvent;

            var concert = await _concertRepository.GetAsync(@event.AggregateId);

            concert.Status = Domain.Concerts.ConcertStatus.PUBLISHED;

            await _concertRepository.UpdateAsync(concert);
        }
    }
}
