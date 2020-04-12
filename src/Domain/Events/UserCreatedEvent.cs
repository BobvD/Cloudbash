using Cloudbash.Domain.SeedWork;
using System;

namespace Cloudbash.Domain.Events
{
    public class UserCreatedEvent : DomainEventBase
    {
        public UserCreatedEvent()
        {
        }

        internal UserCreatedEvent(Guid aggregateId, string username, string email) : base(aggregateId)
        {
            Username = username;
            Email = email;
        }

        public string Username { get; private set; }
        public string Email { get; private set; }

        public override IDomainEvent WithAggregate(Guid aggregateId, long aggregateVersion)
        {
            return new UserCreatedEvent(aggregateId, Username, Email);
        }

        public override string ToString()
        {
            return Username + base.ToString();
        }
    }
}
