﻿using Amazon.Lambda.Core;
using Amazon.Lambda.KinesisEvents;
using Cloudbash.Application.Common.Events;
using Cloudbash.Domain.SeedWork;
using Cloudbash.Infrastructure.EventStore;
using Cloudbash.Lambda.Functions;
using Newtonsoft.Json;
using System;
using System.Text;

namespace Cloudbash.Lambda.Events.Functions
{
    public class EventProcessorFunction : FunctionBase
    {
        // private IEventStore _eventStore;

        public EventProcessorFunction() : base()
        {
            // _eventStore  = (IEventStore)_serviceProvider.GetService(typeof(IEventStore));
        }

        [LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
        public void Run(KinesisEvent kinesisEvent, ILambdaContext context)
        {
            context.Logger.Log("Process event invoked");
            context.Logger.Log("records: " + kinesisEvent.Records.Count);
            foreach (var record in kinesisEvent.Records)
            {
                ProcessRecord(record);
            }
        }

        private void ProcessRecord(KinesisEvent.KinesisEventRecord record)
        {
            // Get the event data from the Kinesis record
            var kinesisRecord = record.Kinesis;
            var dataBytes = kinesisRecord.Data.ToArray();
            var data = Encoding.UTF8.GetString(dataBytes);

            // Log to console for debugging purposes.
            Console.WriteLine($"[{record.EventName}] Data = '{data}'.");

            // Deserialize to dynamic object to get the type of event.
            var enveloppe = JsonConvert.DeserializeObject<KinesisEventEnveloppe>(data);
            // Get the Event Type
            string eventType = enveloppe.Type;
            Type type = Type.GetType(eventType, true);        

            JsonSerializerSettings settings = new JsonSerializerSettings { ContractResolver = new PrivateSetterContractResolver() };

            var @event = JsonConvert.DeserializeObject(enveloppe.Event, type, settings);

            Consume(@event as IDomainEvent);
        }

        private class KinesisEventEnveloppe
        {
            public string Type { get; set; }
            public string Event { get; set; }
        }

        public T CastObject<T>(object input)
        {
            return (T) input;
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
