using Cloudbash.Domain.SeedWork;
using System;
using System.Threading.Tasks;

namespace Cloudbash.Application.Common.Interfaces
{
    public interface IPublisher : IDisposable
    {
        Task PublishAsync(IDomainEvent domainEvent);    
    }
}
