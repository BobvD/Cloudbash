using Cloudbash.Application.Common.Events;
using Cloudbash.Domain.SeedWork;
using Cloudbash.Infrastructure.EventStore;
using Cloudbash.Lambda.Functions;
using Newtonsoft.Json;
using System;

namespace Cloudbash.Lambda.Events.Functions
{
    public abstract class EventProcessorFunction : FunctionBase
    {
        public T CastObject<T>(object input)
        {
            return (T)input;
        }

        public Type TypeFromString(string type)
        {
            return Type.GetType(type, true); 
        }

        public IDomainEvent DeserializeEvent(string @event, Type type)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings { ContractResolver = new PrivateSetterContractResolver() };
            return JsonConvert.DeserializeObject(@event, type, settings) as IDomainEvent;
        }

        public void Consume(IDomainEvent @event)
        {
            var domainEventNotification = CreateDomainEventNotification((dynamic)@event);
            Mediator.Publish(domainEventNotification);
        }

        private static DomainEventNotification<TDomainEvent> CreateDomainEventNotification<TDomainEvent>(TDomainEvent domainEvent)
            where TDomainEvent : IDomainEvent
        {
            return new DomainEventNotification<TDomainEvent>(domainEvent);
        }
    }
}
