using Cloudbash.Application.Common.Events;
using Cloudbash.Domain.Events;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Cloudbash.Application.Concerts.Events
{
    class ConcertCreatedEventHandler : INotificationHandler<DomainEventNotification<ConcertCreatedEvent>>
    {
        public Task Handle(DomainEventNotification<ConcertCreatedEvent> notification, CancellationToken cancellationToken)
        {
            Console.WriteLine("EVENT HANDLER TRIGGERED");
            Console.WriteLine(notification.DomainEvent.ToString());
            return null;
        }
    }
}
