using Cloudbash.Domain.SeedWork;
using Cloudbash.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cloudbash.Infrastructure.EventStore
{
    public class InMemoryEventStore : IEventStore
    {
        private readonly List<IDomainEvent> Events;


        public InMemoryEventStore()
        {
            Events = new List<IDomainEvent>();
        }

        public Task<IEnumerable<IDomainEvent>> GetAsync(Guid aggregateId, long minVersion, long maxVersion)
        {
            var result = Events.Where(e => e.AggregateId == aggregateId).OrderBy(x => x.AggregateVersion);
            return Task.FromResult(result.AsEnumerable());
        }

        public Task SaveAsync(IDomainEvent @event)
        {
            Events.Add(@event);
            return Task.CompletedTask;
        }
    }
}
