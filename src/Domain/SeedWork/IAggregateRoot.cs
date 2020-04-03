using System;
using System.Collections.Generic;

namespace Cloudbash.Domain.SeedWork
{
    public interface IAggregateRoot
    {
        Guid Id { get; }
        long Version { get; }
        void AddEvent(IDomainEvent @event);
        void ApplyEvent(IDomainEvent @event);
        IEnumerable<IDomainEvent> GetUncommittedEvents();
        void ClearUncommittedEvents();

    }
}
