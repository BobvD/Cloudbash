using System;
using System.Collections.Generic;

namespace Cloudbash.Domain.SeedWork
{
    public interface IAggregateRoot
    {
        Guid Id { get; }
        long Version { get; }
        DateTime Created { get; }
        void ApplyEvent(IDomainEvent @event, long version);
        IEnumerable<IDomainEvent> GetUncommittedEvents();
        void ClearUncommittedEvents();

    }
}
