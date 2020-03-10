
using System;

namespace Cloudbash.Domain.SeedWork
{
    public interface IDomainEvent
    {
       Guid EventId { get; }
       Guid AggregateId { get; }      
       long AggregateVersion { get; }
    }
}
