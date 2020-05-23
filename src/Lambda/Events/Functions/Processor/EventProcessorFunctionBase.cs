using Cloudbash.Application.Common.Events;
using Cloudbash.Domain.SeedWork;
using Cloudbash.Infrastructure.EventStore;
using Cloudbash.Lambda.Functions;
using Newtonsoft.Json;
using System;

namespace Cloudbash.Lambda.Events.Functions.Processor
{
    public abstract class EventProcessorFunctionBase : FunctionBase
    {
        public void Consume(string eventEnveloppe)
        {
            try
            {
                var @event = DomainFromEnveloppe(eventEnveloppe);
                Publish(@event);
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to publish Event: " + eventEnveloppe);
            }
          
        }

        public void Consume(string ev, string type)
        {
            try
            {
                var @event = DomainFromEnveloppe(new EventEnveloppe { Event = ev, Type = type + ", Cloudbash.Domain" });
                Publish(@event);
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to publish Event: " + ev);
            }
            
        }

        public void Publish(IDomainEvent @event)
        {            
            var domainEventNotification = CreateDomainEventNotification((dynamic)@event);
            Mediator.Publish(domainEventNotification);
            Console.WriteLine("Event published: " + @event.GetType().Name);
        }

        public IDomainEvent DomainFromEnveloppe(string eventData)
        {
            var enveloppe = JsonConvert.DeserializeObject<EventEnveloppe>(eventData);
            return DomainFromEnveloppe(enveloppe);
        }

        public IDomainEvent DomainFromEnveloppe(EventEnveloppe enveloppe)
        {
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

        public class EventEnveloppe
        {
            public string Type { get; set; }
            public string Event { get; set; }
        }

    }
}
