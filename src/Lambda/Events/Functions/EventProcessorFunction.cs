using Amazon.Lambda.Core;
using Amazon.Lambda.KinesisEvents;
using Cloudbash.Lambda.Functions;

namespace Cloudbash.Lambda.Events.Functions
{
    public class EventProcessorFunction : FunctionBase
    {

        [LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
        public void Run(KinesisEvent kinesisEvent, ILambdaContext context)
        {
            context.Logger.Log("Process event invoked");
            context.Logger.Log("records: " + kinesisEvent.Records.Count);            
        }

        private void ProcessKinesisEvent(KinesisEvent kinesisEvent)
        {
                       
        }

        private async void ProcessRecord(KinesisEvent.KinesisEventRecord record)
        {
          
        }

    }
}
