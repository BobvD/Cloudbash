using Amazon.Lambda.Core;
using Amazon.Lambda.KinesisEvents;
using Cloudbash.Domain.SeedWork;
using Cloudbash.Infrastructure.Persistence;
using Cloudbash.Lambda.Functions;
using Newtonsoft.Json;
using System;
using System.Text;

namespace Cloudbash.Lambda.Events.Functions
{
    public class EventProcessorFunction : FunctionBase
    {
        private IEventStore _eventStore;

        public EventProcessorFunction() : base()
        {
            _eventStore  = (IEventStore)_serviceProvider.GetService(typeof(IEventStore));
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
            var kinesisRecord = record.Kinesis;
            var dataBytes = kinesisRecord.Data.ToArray();
            var dataText = Encoding.UTF8.GetString(dataBytes);
            Console.WriteLine($"[{record.EventName}] Data = '{dataText}'.");            
        }
    }
}
