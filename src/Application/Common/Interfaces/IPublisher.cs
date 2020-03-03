using Cloudbash.Domain.Events;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cloudbash.Application.Common.Interfaces
{
    public interface IPublisher : IDisposable
    {
        Task Publish(DomainEvent domainEvent);
        Task Publish(IEnumerable<DomainEvent> domainEvents, Header header);

        int GetValue();

    }
}
