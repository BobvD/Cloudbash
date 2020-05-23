using Cloudbash.Domain.SeedWork;
using System;

namespace Cloudbash.Domain.Users.Events
{
    public class UserCreatedEvent : DomainEventBase
    {
        public UserCreatedEvent()
        {
        }

        internal UserCreatedEvent(Guid aggregateId, string fullName, string email) 
            : base(aggregateId)
        {
            FullName = fullName;
            Email = email;
        }

        public string FullName { get; private set; }
        public string Email { get; private set; }

        internal UserCreatedEvent(Guid aggregateId, long aggregateVersion, string fullName, string email) 
            : base(aggregateId, aggregateVersion)
        {
            FullName = fullName;
            Email = email;
        }

        public override IDomainEvent WithAggregate(Guid aggregateId, long aggregateVersion)
        {
            return new UserCreatedEvent(aggregateId, aggregateVersion, FullName, Email);
        }

        public override string ToString()
        {
            return FullName + base.ToString();
        }
    }
}
