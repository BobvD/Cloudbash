using Cloudbash.Application.Common.Events;
using Cloudbash.Application.Common.Interfaces;
using Cloudbash.Domain.Concerts.Events;
using Cloudbash.Domain.ViewModels;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Cloudbash.Application.Concerts.Events
{
    public class ConcertTicketTypeAddedEventHandler : INotificationHandler<DomainEventNotification<ConcertTicketTypeAddedEvent>>
    {
        private readonly IViewModelRepository<Concert> _concertRepository;

        public ConcertTicketTypeAddedEventHandler(IViewModelRepository<Concert> concertRepository)
        {
            _concertRepository = concertRepository;
        }

        public async Task Handle(DomainEventNotification<ConcertTicketTypeAddedEvent> notification, CancellationToken cancellationToken)
        {
            var @event = notification.DomainEvent;
            
            var concert = await _concertRepository.GetAsync(@event.AggregateId);
                        
            concert.TicketTypes.Add(
                   new TicketType
                   {
                       Id = @event.Type.Id.ToString(),
                       Name = @event.Type.Name,
                       Price = @event.Type.Price,
                       Quantity = @event.Type.Quantity
                   });

            await _concertRepository.UpdateAsync(concert);
        }
    }
}
