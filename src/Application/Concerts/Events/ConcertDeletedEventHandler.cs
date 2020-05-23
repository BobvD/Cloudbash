using Cloudbash.Application.Common.Events;
using Cloudbash.Application.Common.Interfaces;
using Cloudbash.Domain.ReadModels;
using Cloudbash.Domain.Concerts.Events;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Cloudbash.Application.Concerts.Events
{
    public class ConcertDeletedEventHandler : INotificationHandler<DomainEventNotification<ConcertDeletedEvent>>
    {
        private readonly IViewModelRepository<Concert> _concertRepository;

        public ConcertDeletedEventHandler(IViewModelRepository<Concert> concertRepository)
        {
            _concertRepository = concertRepository;
        }

        public async Task Handle(DomainEventNotification<ConcertDeletedEvent> notification, CancellationToken cancellationToken)
        {
            var @event = notification.DomainEvent;       
            await _concertRepository.DeleteAsync(@event.AggregateId);            
        }
    }
}
