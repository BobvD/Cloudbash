using Amazon.KinesisFirehose;
using Amazon.KinesisFirehose.Model;
using Amazon.Lambda.Core;
using Amazon.Lambda.KinesisEvents;
using Cloudbash.Infrastructure;
using Cloudbash.Lambda.Functions;
using System;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Threading.Tasks;

namespace Cloudbash.Lambda.Events.Functions
{
    public class KinesisToFirehoseFunction : FunctionBase
    {       
        private readonly AmazonKinesisFirehoseClient _amazonKinesisFirehoseClient;

        public KinesisToFirehoseFunction(IAwsClientFactory<AmazonKinesisFirehoseClient> clientFactory) : base()
        {
            _amazonKinesisFirehoseClient = clientFactory.GetAwsClient();
        }        

        [LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
        public async void Run(KinesisEvent kinesisEvent)
        {
            foreach (var record in kinesisEvent.Records)
            {
                var dataBytes = record.Kinesis.Data.ToArray();
                // var eventData = Encoding.UTF8.GetString(dataBytes);
                await WriteAsync(dataBytes);
            }
        }

        public async Task WriteAsync(byte[] data)
        {
            var req = new PutRecordRequest
            {
                DeliveryStreamName = "firehose",
                Record = new Record
                {
                    Data = Compress(data)
                }
            };

            try
            {
                await _amazonKinesisFirehoseClient.PutRecordAsync(req);
            }
            catch (AmazonKinesisFirehoseException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static MemoryStream Compress(byte[] data)
        {
            var buffer = new MemoryStream();
            using (var gzip = new GZipStream(buffer, CompressionMode.Compress, true))
            {
                gzip.Write(data, 0, data.Length);
            }
            buffer.Position = 0;
            return buffer;
        }

    }
}


