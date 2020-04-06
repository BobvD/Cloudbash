﻿using Cloudbash.Application.Common.Events;
using Cloudbash.Application.Common.Interfaces;
using Cloudbash.Domain.ViewModels;
using Cloudbash.Domain.Events;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System;

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
            await _concertRepository.InsertAsync(new Concert { Id = @event.AggregateId, Name = @event.Name }, cancellationToken);            
        }
    }
}
