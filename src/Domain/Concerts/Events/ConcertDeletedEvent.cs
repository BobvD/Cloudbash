﻿using Cloudbash.Domain.SeedWork;
using System;

namespace Cloudbash.Domain.Concerts.Events
{
    public class ConcertDeletedEvent : DomainEventBase
    {
        public ConcertDeletedEvent() { }

        internal ConcertDeletedEvent(Guid aggregateId) : base(aggregateId) { }

        internal ConcertDeletedEvent(Guid aggregateId, long aggregateVersion) : base(aggregateId, aggregateVersion) { }

        public override IDomainEvent WithAggregate(Guid aggregateId, long aggregateVersion)
        {
            return new ConcertDeletedEvent(aggregateId, aggregateVersion);
        }
    }
}
