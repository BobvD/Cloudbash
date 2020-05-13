using Amazon.Lambda.Core;
using Amazon.Lambda.KinesisEvents;
using Cloudbash.Infrastructure.Firehose;

namespace Cloudbash.Lambda.Events.Functions.Firehose
{
    public class KinesisToFirehoseFunction : FirehoseFunction
    {

        [LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
        public async void Run(KinesisEvent kinesisEvent)
        {
            foreach (var record in kinesisEvent.Records)
            {
                var dataBytes = record.Kinesis.Data.ToArray();
                // var eventData = Encoding.UTF8.GetString(dataBytes);
                // await WriteAsync(dataBytes);
            }
        }       

    }
}


