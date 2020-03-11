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
            SaveEventAsync(dataText);
        }

        private async void SaveEventAsync(string e)
        {

            Console.WriteLine("START SAVING EVENT");
            EventRecord @event = null;

            try
            {
                @event  = JsonConvert.DeserializeObject<EventRecord>(e);
            }
            catch (Exception ex)
            {

                Console.WriteLine("FAILURE TO DESERIALIZE EVENT");
                Console.WriteLine(ex.Message);
            }
            

            IEventStore eventStore = null;

            try
            {
                eventStore  = (IEventStore)_serviceProvider.GetService(typeof(IEventStore));
            }
            catch (Exception ex)
            {
                Console.WriteLine("FAILURE TO GET EVENTSTORE");
                Console.WriteLine(ex.Message);
            }
                       
            Console.WriteLine("SAVING EVENT");

            try
            {

                await eventStore.SaveAsync(@event, new System.Threading.CancellationToken());
            }
            catch (Exception ex)
            {
                Console.WriteLine("FAILURE TO SAVE EVENT");
                Console.WriteLine(ex.Message);
            }
            
        }
    }
}
