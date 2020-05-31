using Amazon.KinesisFirehose;
using Amazon.KinesisFirehose.Model;
using Cloudbash.Infrastructure.Configs;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Cloudbash.Infrastructure.Firehose
{
    public class FirehoseClient : IFirehoseClient
    {
        private readonly AmazonKinesisFirehoseClient _amazonKinesisFirehoseClient;
        private readonly IServerlessConfiguration _config;
        private readonly ILogger<FirehoseClient> _logger;
        public FirehoseClient(IAwsClientFactory<AmazonKinesisFirehoseClient> clientFactory,
                              IServerlessConfiguration config,
                              ILogger<FirehoseClient> logger)
        {
            _amazonKinesisFirehoseClient = clientFactory.GetAwsClient();
            _config = config;
            _logger = logger;
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
                await _amazonKinesisFirehoseClient.PutRecordAsync(req);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
    }
}
