﻿using Amazon.Lambda.Core;
using Amazon.Lambda.DynamoDBEvents;
using Amazon.Lambda.Serialization.Json;
using Newtonsoft.Json;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Serializer = Amazon.Lambda.Serialization.Json.JsonSerializer;

namespace Cloudbash.Lambda.Events.Functions.Firehose
{
    public class DynamoToFirehose : FirehoseFunction
    {
        private static readonly Serializer _jsonSerializer = new Serializer();

        [LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
        public async Task Run(DynamoDBEvent dynamoEvent)
        {
            foreach (var record in dynamoEvent.Records)
            {
                if (record.EventName == "INSERT")
                {
                    var image = record.Dynamodb.NewImage;
                    var @event = new
                    {
                        AggregateVersion = image["AggregateVersion"].N,
                        EventType = image["EventType"].S,
                        AggregateId = image["AggregateId"].S,
                        Data = image["Data"].S,
                        Created = image["Created"].S
                    };

                    var json = JsonConvert.SerializeObject(@event) + "\n";
                    await _client.WriteAsync(Encoding.UTF8.GetBytes((json)));        
                     
                }
                else
                {
                    continue;
                }
            }
        }

        private byte[] SerializeObject(object streamRecord)
        {
            using (var ms = new MemoryStream())
            {
                _jsonSerializer.Serialize(streamRecord, ms);
                return ms.ToArray();
            }
        }

    }
}
