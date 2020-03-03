using System;

namespace Cloudbash.Domain.Events
{
    public class DomainEvent : IDomainEvent
    {
        public Guid Id { get; set; }
    }
}
