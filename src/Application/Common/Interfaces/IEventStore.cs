
using Cloudbash.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cloudbash.Infrastructure.Persistence
{
    public interface IEventStore
    {
        Task SaveAsync(IDomainEvent e);
        Task<IEnumerable<IDomainEvent>> GetAsync(Guid aggregateId);
    }
}
