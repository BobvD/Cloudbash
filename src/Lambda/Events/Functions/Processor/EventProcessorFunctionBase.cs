using Amazon.Lambda.Core;
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
        protected void Consume(string eventEnveloppe)
        {
            try
            {
                var @event = DomainFromEnveloppe(eventEnveloppe);
                Publish(@event);
            }
            catch (Exception ex)
            {
                LambdaLogger.Log("Failed to consume event: " + eventEnveloppe);
                LambdaLogger.Log(ex.Message);
            }
          
        }

        protected void Consume(string ev, string type)
        {
            try
            {
                var @event = DomainFromEnveloppe(new EventEnveloppe { Event = ev, Type = type + ", Cloudbash.Domain" });
                Publish(@event);
            }
            catch (Exception ex)
            {
                LambdaLogger.Log("Failed to consume event: " + ev);
                LambdaLogger.Log(ex.Message);
            }
            
        }

        protected void Publish(IDomainEvent @event)
        {            
            var domainEventNotification = CreateDomainEventNotification((dynamic)@event);
            Mediator.Publish(domainEventNotification);
            LambdaLogger.Log("Event published: " + @event.GetType().Name);
        }

        protected static IDomainEvent DomainFromEnveloppe(string eventData)
        {
            var enveloppe = JsonConvert.DeserializeObject<EventEnveloppe>(eventData);
            return DomainFromEnveloppe(enveloppe);
        }

        protected static IDomainEvent DomainFromEnveloppe(EventEnveloppe enveloppe)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings { ContractResolver = new PrivateSetterContractResolver() };
            return JsonConvert.DeserializeObject(enveloppe.Event, TypeFromString(enveloppe.Type), settings) as IDomainEvent;
        }

        protected static Type TypeFromString(string type)
        {
            return Type.GetType(type, true);
        }

      
        private static DomainEventNotification<TDomainEvent> CreateDomainEventNotification<TDomainEvent>(TDomainEvent domainEvent)
            where TDomainEvent : IDomainEvent
        {
            return new DomainEventNotification<TDomainEvent>(domainEvent);
        }

        protected class EventEnveloppe
        {
            public string Type { get; set; }
            public string Event { get; set; }
        }

    }
}
