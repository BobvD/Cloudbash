
// source: https://stackoverflow.com/questions/47292941/ddd-referencing-mediatr-interface-from-the-domain-project

using Cloudbash.Domain.SeedWork;
using MediatR;

namespace Cloudbash.Application.Common.Events
{
    public class DomainEventNotification<TDomainEvent> : INotification where TDomainEvent : IDomainEvent
    {
        public TDomainEvent DomainEvent { get; }

        public DomainEventNotification(TDomainEvent domainEvent)
        {
            DomainEvent = domainEvent;
        }
    }
}