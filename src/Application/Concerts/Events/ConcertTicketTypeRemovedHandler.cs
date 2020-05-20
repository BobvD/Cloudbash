using Cloudbash.Application.Common.Events;
using Cloudbash.Application.Common.Interfaces;
using Cloudbash.Domain.Concerts.Events;
using Cloudbash.Domain.ViewModels;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Cloudbash.Application.Concerts.Events
{
    public class ConcertTicketTypeRemovedHandler : INotificationHandler<DomainEventNotification<ConcertTicketTypeRemoved>>
    {
        private readonly IViewModelRepository<Concert> _concertRepository;

        public ConcertTicketTypeRemovedHandler(IViewModelRepository<Concert> concertRepository)
        {
            _concertRepository = concertRepository;
        }

        public async Task Handle(DomainEventNotification<ConcertTicketTypeRemoved> notification, CancellationToken cancellationToken)
        {
            var @event = notification.DomainEvent;
            var children = new string[] { "TicketTypes" };
            var concert = await _concertRepository.GetAsync(@event.AggregateId, children);

            var ticketType = concert.TicketTypes
                .SingleOrDefault(t => t.Id == @event.TicketTypeId.ToString());

            if (ticketType != null)
            {
                concert.TicketTypes.Remove(ticketType);
            }
            
            await _concertRepository.UpdateAsync(concert);
           
        }
    }
}
