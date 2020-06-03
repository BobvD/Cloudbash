
using Cloudbash.Domain.SeedWork;
using System;

namespace Cloudbash.Domain.Users.Events
{
    public class UserAuthenticatedEvent : DomainEventBase
    {
        public UserAuthenticatedEvent(){ }

        internal UserAuthenticatedEvent(Guid aggregateId) 
            : base(aggregateId) { }
        internal UserAuthenticatedEvent(Guid aggregateId, long aggregateVersion) 
            : base(aggregateId, aggregateVersion) { }

        public override IDomainEvent WithAggregate(Guid aggregateId, long aggregateVersion)
        {
            return new UserAuthenticatedEvent(aggregateId, aggregateVersion);
        }

    }
}
