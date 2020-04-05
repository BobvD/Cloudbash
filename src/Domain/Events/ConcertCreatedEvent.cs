using Cloudbash.Domain.SeedWork;
using System;

namespace Cloudbash.Domain.Events
{
    public class ConcertCreatedEvent : DomainEventBase
    {
        public ConcertCreatedEvent()
        {
        }

        internal ConcertCreatedEvent(Guid aggregateId, string title) : base(aggregateId)
        {
            Title = title;
        }
        
        public string Title { get; private set; }

        public override IDomainEvent WithAggregate(Guid aggregateId, long aggregateVersion)
        {
            return new ConcertCreatedEvent(aggregateId, Title);
        }
    }
}
