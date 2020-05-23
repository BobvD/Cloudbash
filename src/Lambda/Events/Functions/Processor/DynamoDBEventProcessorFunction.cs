using Amazon.DynamoDBv2.Model;
using Amazon.Lambda.Core;
using Amazon.Lambda.DynamoDBEvents;

namespace Cloudbash.Lambda.Events.Functions.Processor
{
    public class DynamoDBEventProcessorFunction : EventProcessorFunctionBase
    {
        [LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
        public void Run(DynamoDBEvent dynamoEvent)
        {
            foreach (var record in dynamoEvent.Records)
            {
                if (record.EventName == "INSERT")
                {
                    AttributeValue @event = null;
                    AttributeValue type = null;
                    record.Dynamodb.NewImage.TryGetValue("Data", out @event);
                    record.Dynamodb.NewImage.TryGetValue("EventType", out type);                   
                    Consume(@event.S, type.S);
                } else
                {
                    continue;
                }
            }
        }
    }
}
