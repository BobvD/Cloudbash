using Amazon.KinesisFirehose;
using Amazon.KinesisFirehose.Model;
using Cloudbash.Infrastructure.Configs;
using System;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;

namespace Cloudbash.Infrastructure.Firehose
{
    public class FirehoseClient : IFirehoseClient
    {
        private readonly AmazonKinesisFirehoseClient _amazonKinesisFirehoseClient;
        private readonly IServerlessConfiguration _config;
        public FirehoseClient(IAwsClientFactory<AmazonKinesisFirehoseClient> clientFactory,
                              IServerlessConfiguration config)
        {
            _amazonKinesisFirehoseClient = clientFactory.GetAwsClient();
            _config = config;
        }

        public async Task WriteAsync(byte[] data)
        {
            var req = new PutRecordRequest
            {
                DeliveryStreamName = _config.ConfigName + "-firehose",
                Record = new Record
                {
                    Data = new MemoryStream(data)
                }
            };

            try
            {
                var res = await _amazonKinesisFirehoseClient.PutRecordAsync(req);
                Console.WriteLine(res.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private MemoryStream Compress(byte[] data)
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
