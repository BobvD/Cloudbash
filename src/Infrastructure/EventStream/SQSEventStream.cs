using Cloudbash.Application.Common.Interfaces;
using Cloudbash.Domain.SeedWork;
using System.Threading.Tasks;

namespace Cloudbash.Infrastructure.EventStream
{
    public class SQSEventStream : IPublisher
    {
        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

        public Task PublishAsync(IDomainEvent domainEvent)
        {
            throw new System.NotImplementedException();
        }
    }
}
