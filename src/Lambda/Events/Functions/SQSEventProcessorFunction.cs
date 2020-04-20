using Amazon.Lambda.Core;
using Amazon.Lambda.SQSEvents;
using System;

namespace Cloudbash.Lambda.Events.Functions
{
    public class SQSEventProcessorFunction
    {
        [LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
        public void Run(SQSEvent sqsEvent, ILambdaContext context)
        {
            Console.WriteLine($"Beginning to process {sqsEvent.Records.Count} records...");

            foreach (var record in sqsEvent.Records)
            {
                Console.WriteLine($"Message ID: {record.MessageId}");
                Console.WriteLine($"Event Source: {record.EventSource}");

                Console.WriteLine($"Record Body:");
                Console.WriteLine(record.Body);
                string eventType = "";
                record.Attributes.TryGetValue("Type", out eventType);
                Console.WriteLine(eventType);
                Type type = Type.GetType(eventType, true);

                
            }

            Console.WriteLine("Processing complete.");

            Console.WriteLine($"Processed {sqsEvent.Records.Count} records.");
        }
    }
}
