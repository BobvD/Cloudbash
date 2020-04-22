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
        public void Consume(string eventData)
        {
            var @event = DeserializeEvent(eventData);
            var domainEventNotification = CreateDomainEventNotification((dynamic)@event);
            Mediator.Publish(domainEventNotification);
        }

        public IDomainEvent DeserializeEvent(string eventData)
        {
            var enveloppe = JsonConvert.DeserializeObject<EventEnveloppe>(eventData);

            JsonSerializerSettings settings = new JsonSerializerSettings { ContractResolver = new PrivateSetterContractResolver() };
            return JsonConvert.DeserializeObject(enveloppe.Event, TypeFromString(enveloppe.Type), settings) as IDomainEvent;
        }

        public Type TypeFromString(string type)
        {
            return Type.GetType(type, true);
        }
      
        private static DomainEventNotification<TDomainEvent> CreateDomainEventNotification<TDomainEvent>(TDomainEvent domainEvent)
            where TDomainEvent : IDomainEvent
        {
            return new DomainEventNotification<TDomainEvent>(domainEvent);
        }

        private class EventEnveloppe
        {
            public string Type { get; set; }
            public string Event { get; set; }
        }

    }
}
