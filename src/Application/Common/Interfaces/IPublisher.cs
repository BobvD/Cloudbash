using Cloudbash.Domain.SeedWork;
using System.Threading.Tasks;

namespace Cloudbash.Application.Common.Interfaces
{
    public interface IPublisher
    {
        Task PublishAsync(IDomainEvent domainEvent);    
    }
}
