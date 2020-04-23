﻿using Amazon.Kinesis;
using Amazon.Kinesis.Model;
using Cloudbash.Domain.SeedWork;
using Cloudbash.Infrastructure.Configs;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Cloudbash.Infrastructure.EventStream
{
    public class KinesisEventStream : EventStream
    {

        private readonly AmazonKinesisClient _amazonKinesisClient;
        private readonly IServerlessConfiguration _config;
        public KinesisEventStream(IAwsClientFactory<AmazonKinesisClient> clientFactory, IServerlessConfiguration config)
        {
            _amazonKinesisClient = clientFactory.GetAwsClient();
            _config = config;
        }
              
        public override Task PublishAsync(IDomainEvent @event)
        {            
            PutRecordRequest requestRecord = new PutRecordRequest();

            requestRecord.StreamName = _config.KinesisStreamName;
            requestRecord.PartitionKey = _config.KinesisPartitionKey;
            requestRecord.Data = new MemoryStream(Encoding.UTF8.GetBytes(CreateEnveloppe(@event)));
                  
            var result = _amazonKinesisClient.PutRecordAsync(requestRecord).Result;

            return Task.FromResult(result);
        }
        
    }
}