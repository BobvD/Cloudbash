using Amazon.Lambda.Core;
using Amazon.Lambda.DynamoDBEvents;

namespace Cloudbash.Lambda.Events.Functions
{
    public class DynamoDBEventProcessorFunction : EventProcessorFunction
    {
        [LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
        public void Run(DynamoDBEvent dynamoEvent)
        {
           LambdaLogger.Log($"*** INFO: Event: " + dynamoEvent.ToString());
        }
    }
}
