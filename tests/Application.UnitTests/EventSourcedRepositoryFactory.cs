using Cloudbash.Application.Common.EventSourcing;
using Cloudbash.Application.Common.Interfaces;
using Cloudbash.Domain.SeedWork;
using Cloudbash.Infrastructure.Configs;
using Cloudbash.Infrastructure.EventStore;
using Moq;

namespace Cloudbash.Application.UnitTests
{
    public class EventSourcedRepositoryFactory<TAggregate>
        where TAggregate : AggregateRootBase, IAggregateRoot
    {
        public static EventSourcedRepository<TAggregate> Create()
        {
            var eventStore = new InMemoryEventStore();
            var publisherMock = new Mock<IPublisher>();
            var configMock = new Mock<IServerlessConfiguration>();

            var repo = new EventSourcedRepository<TAggregate>(eventStore, publisherMock.Object, configMock.Object);
           
            return repo;
        }


    }
}
