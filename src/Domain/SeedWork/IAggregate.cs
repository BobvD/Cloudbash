using System;
using System.Collections.Generic;

namespace Cloudbash.Domain.SeedWork
{
    public interface IAggregate
    {
        Guid Id { get; }
        long Version { get; }
        void ApplyEvent(IDomainEvent @event, long version);
        IEnumerable<IDomainEvent> GetUncommittedEvents();
        void ClearUncommittedEvents();
    }
}
