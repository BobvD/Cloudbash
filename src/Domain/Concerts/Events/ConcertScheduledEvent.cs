using Cloudbash.Domain.SeedWork;
using System;

namespace Cloudbash.Domain.Concerts.Events
{
    public class ConcertScheduledEvent : DomainEventBase
    {
     
        public ConcertScheduledEvent() { }

        internal ConcertScheduledEvent(Guid aggregateId, DateTime startDate, DateTime endDate)
            : base(aggregateId)
        {
            StartDate = startDate;
            EndDate = endDate;
        }

        internal ConcertScheduledEvent(Guid aggregateId, long aggregateVersion, DateTime startDate, DateTime endDate)
            : base(aggregateId, aggregateVersion)
        {
            StartDate = startDate;
            EndDate = endDate;
        }

        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }


        public override IDomainEvent WithAggregate(Guid aggregateId, long aggregateVersion)
        {
            return new ConcertScheduledEvent(aggregateId, aggregateVersion, StartDate, EndDate);
        }
    }
}
