
using Cloudbash.Domain.SeedWork;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cloudbash.Infrastructure.Persistence
{
    public class DynamoDBEventStore : IEventStore
    {
        public Task<IEnumerable<IDomainEvent>> GetAsync(string aggregateId, string aggregateType, int fromVersion)
        {
            throw new System.NotImplementedException();
        }

        public Task SaveAsync(IDomainEvent e)
        {
            throw new System.NotImplementedException();
        }
    }
}
