using Cloudbash.Application.Common.Interfaces;
using Cloudbash.Domain.SeedWork;
using System;
using System.Threading.Tasks;

namespace Cloudbash.Application.Common.EventSourcing
{
    public class Repository<TAggregate> : IRepository<TAggregate>
        where TAggregate : Aggregate, IAggregate
    {
        private readonly IPublisher _publisher;

        public Repository(IPublisher publisher)
        {
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
                IAggregate aggregatePersistence = aggregate;

                foreach (var @event in aggregatePersistence.GetUncommittedEvents())
                {
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
