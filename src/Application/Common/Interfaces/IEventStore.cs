
using Cloudbash.Domain.SeedWork;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Cloudbash.Infrastructure.Persistence
{
    public interface IEventStore
    {
        Task SaveAsync(EventRecord e, CancellationToken c);
        Task<IEnumerable<IDomainEvent>> GetAsync(string aggregateId, string aggregateType, int fromVersion);
    }
}
