using Amazon.Lambda.Core;
using Amazon.Lambda.KinesisEvents;
using Newtonsoft.Json;
using System.Text;

namespace Cloudbash.Lambda.Events.Functions
{
    public class KinesisEventProcessorFunction : EventProcessorFunction
    {

        [LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
        public override void Run(KinesisEvent kinesisEvent)
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
