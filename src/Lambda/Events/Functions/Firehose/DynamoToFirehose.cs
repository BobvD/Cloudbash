using Amazon.Lambda.Core;
using Amazon.Lambda.DynamoDBEvents;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;

namespace Cloudbash.Lambda.Events.Functions.Firehose
{
    public class DynamoToFirehose : FirehoseFunction
    {

        [LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
        public async Task Run(DynamoDBEvent dynamoEvent)
        {
            foreach (var record in dynamoEvent.Records)
            {
                if (record.EventName == "INSERT")
                {
                    await _client.WriteAsync(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(record.Dynamodb.NewImage)));
                }
                else
                {
                    continue;
                }
            }
        }

    }
}
