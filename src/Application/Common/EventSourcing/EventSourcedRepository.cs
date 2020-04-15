using Cloudbash.Application.Common.Interfaces;
using Cloudbash.Domain.SeedWork;
using Cloudbash.Infrastructure.Persistence;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace Cloudbash.Application.Common.EventSourcing
{
    public class EventSourcedRepository<TAggregate> : IRepository<TAggregate>
        where TAggregate : AggregateRootBase, IAggregateRoot
    {
        private readonly IEventStore _eventStore;
        private readonly IPublisher _publisher;

        public EventSourcedRepository(
            IEventStore eventStore,
            IPublisher publisher)
        {
            _eventStore = eventStore;
            _publisher = publisher;
        }

        public async Task<TAggregate> GetByIdAsync(Guid id)
        {            
            var aggregate = CreateEmptyAggregate();
            IAggregateRoot aggregatePersistence = aggregate;

            foreach (var @event in await _eventStore.GetAsync(id))
            {
                aggregatePersistence.ApplyEvent(@event);
            }
            return aggregate;           
        }

        public async Task SaveAsync(TAggregate aggregate)
        {            
            // Loop to all new (uncommited) events
            foreach (var @event in aggregate.GetUncommittedEvents())
            {
                // Save event to Event Store
                await _eventStore.SaveAsync(@event);
                // Publish event to Event Bus
                await _publisher.PublishAsync((dynamic)@event);
            }
            // All events are committed, clear the list with uncommitted events
            aggregate.ClearUncommittedEvents();           
        }

        private TAggregate CreateEmptyAggregate()
        {
            return (TAggregate)typeof(TAggregate)
                    .GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public,
                        null, new Type[0], new ParameterModifier[0])
                    .Invoke(new object[0]);
        }

    }
}
