
// source: https://github.com/VenomAV/EventSourcingCQRS/blob/a2046e3017ff2f2ee5287d420aa0ab444f69cfc0/EventSourcingCQRS.Domain/Core/IDomainEvent.cs
using System;

namespace Cloudbash.Domain.SeedWork
{
    public interface IDomainEvent
    {
        /// <summary>
        /// The event identifier
        /// </summary>
        Guid EventId { get; }

        /// <summary>
        /// The identifier of the aggregate which has generated the event
        /// </summary>
        Guid AggregateId { get; }

        /// <summary>
        /// The version of the aggregate when the event has been generated
        /// </summary>
        long AggregateVersion { get; }
    }
}
