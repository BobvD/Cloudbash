using Amazon.Lambda.Core;
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
    public class KinesisEventProcessorFunction : EventProcessorFunction
    {

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
            var enveloppe = JsonConvert.DeserializeObject<KinesisEventEnveloppe>(data);     
            var @event = DeserializeEvent(enveloppe.Event, TypeFromString(enveloppe.Type));

            Consume(@event as IDomainEvent);
        }

        private class KinesisEventEnveloppe
        {
            public string Type { get; set; }
            public string Event { get; set; }
        }

        

    }
}
