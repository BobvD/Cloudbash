using Cloudbash.Application.Common.Interfaces;
using Cloudbash.Domain.SeedWork;
using Cloudbash.Infrastructure.Persistence;
using System;
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

        public Task<TAggregate> GetByIdAsync(Guid id)
        {
            throw new System.NotImplementedException();
        }

        public async Task SaveAsync(TAggregate aggregate)
        {
            try
            {
                IAggregateRoot aggregatePersistence = aggregate;

                foreach (var @event in aggregatePersistence.GetUncommittedEvents())
                {
                    await _eventStore.SaveAsync(@event);
                    await _publisher.PublishAsync((dynamic)@event);
                }
                aggregatePersistence.ClearUncommittedEvents();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
