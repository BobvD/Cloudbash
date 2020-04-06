using Cloudbash.Domain.SeedWork;
using System;

namespace Cloudbash.Domain.Events
{
    public class ConcertCreatedEvent : DomainEventBase
    {
        public ConcertCreatedEvent()
        {
        }

        internal ConcertCreatedEvent(Guid aggregateId, string name) : base(aggregateId)
        {
            Name = name;
        }
        
        public string Name { get; private set; }

        public override IDomainEvent WithAggregate(Guid aggregateId, long aggregateVersion)
        {
            return new ConcertCreatedEvent(aggregateId, Name);
        }

        public override string ToString()
        {
            return Name + base.ToString();
        }
    }
}
