using Amazon.Lambda.Core;
using Amazon.Lambda.SQSEvents;
using static Amazon.Lambda.SQSEvents.SQSEvent;

namespace Cloudbash.Lambda.Events.Functions.Processor
{
    public class SQSEventProcessorFunction : EventProcessorFunction
    {
        [LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
        public void Run(SQSEvent sqsEvent)
        {
            foreach (var record in sqsEvent.Records)
            {
                Consume(record.Body);
            }
        }

    }
}
