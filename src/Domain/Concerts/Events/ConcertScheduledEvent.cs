using Cloudbash.Domain.SeedWork;
using System;

namespace Cloudbash.Domain.Concerts.Events
{
    public class ConcertScheduledEvent : DomainEventBase
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

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

        public override IDomainEvent WithAggregate(Guid aggregateId, long aggregateVersion)
        {
            return new ConcertScheduledEvent(aggregateId, aggregateVersion, StartDate, EndDate);
        }
    }
}
