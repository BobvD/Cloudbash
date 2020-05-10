using Cloudbash.Application.Common.Events;
using Cloudbash.Application.Common.Interfaces;
using Cloudbash.Domain.ViewModels;
using Cloudbash.Domain.Concerts.Events;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace Cloudbash.Application.Concerts.Events
{
    public class ConcertCreatedEventHandler : INotificationHandler<DomainEventNotification<ConcertCreatedEvent>>
    {
        private readonly IViewModelRepository<Concert> _concertRepository;
        private readonly IViewModelRepository<Venue> _venueRepository;

        public ConcertCreatedEventHandler(IViewModelRepository<Concert> concertRepository,
                                          IViewModelRepository<Venue> venueRepository)
        {
            _concertRepository = concertRepository;
            _venueRepository = venueRepository;
        }

        public async Task Handle(DomainEventNotification<ConcertCreatedEvent> notification, CancellationToken cancellationToken)
        {
            Console.WriteLine("ConcertCreatedEventHandler called");
            var @event = notification.DomainEvent;
            var venue = await _venueRepository.GetAsync(@event.VenueId);
            Console.WriteLine("Venue name: " + venue.Name);
            await _concertRepository.AddAsync(
                new Concert { 
                    Id = @event.AggregateId.ToString(), 
                    Name = @event.Name, 
                    VenueId = @event.VenueId.ToString(), 
                    Venue = venue,
                    ImageUrl = @event.ImageUrl, 
                    Date = @event.Date 
                });            
        }
    }
}
