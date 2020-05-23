using Amazon.Lambda.Core;
using Amazon.Lambda.KinesisEvents;
using System.Text;

namespace Cloudbash.Lambda.Events.Functions.Processor
{
    public class KinesisEventProcessorFunction : EventProcessorFunctionBase
    {

        [LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
        public void Run(KinesisEvent kinesisEvent)
        {
            foreach (var record in kinesisEvent.Records)
            {
                var dataBytes = record.Kinesis.Data.ToArray();
                var eventData = Encoding.UTF8.GetString(dataBytes);

                Consume(eventData);
            }
        }
        
    }
}
