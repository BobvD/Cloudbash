using Amazon.Lambda.Core;
using Amazon.Lambda.SQSEvents;
using System;
using static Amazon.Lambda.SQSEvents.SQSEvent;

namespace Cloudbash.Lambda.Events.Functions
{
    public class SQSEventProcessorFunction : EventProcessorFunction
    {
        [LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
        public void Run(SQSEvent sqsEvent)
        {
            foreach (var record in sqsEvent.Records)
            {
                MessageAttribute eventType = null;
                record.MessageAttributes.TryGetValue("Type", out eventType);
                var @event = DeserializeEvent(record.Body, TypeFromString(eventType.StringValue));
                Consume(@event);
            }
        }
    }
}
