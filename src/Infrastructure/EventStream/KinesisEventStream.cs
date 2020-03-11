using Amazon;
using Amazon.Kinesis;
using Amazon.Kinesis.Model;
using Cloudbash.Application.Common.Interfaces;
using Cloudbash.Domain.SeedWork;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Cloudbash.Infrastructure.EventStream
{
    public class KinesisEventStream : IPublisher
    {

        private readonly AmazonKinesisClient _amazonKinesisClient;
        private readonly string _streamName;

        public KinesisEventStream(IAwsClientFactory<AmazonKinesisClient> clientFactory)
        {
            _amazonKinesisClient = clientFactory.GetAwsClient();
            _streamName = "eventStream";
        }
              
        public Task PublishAsync(IDomainEvent @event)
        {
            PutRecordRequest requestRecord = new PutRecordRequest();
            requestRecord.StreamName = _streamName;           
            requestRecord.Data = new MemoryStream(createRecord(@event));
            requestRecord.PartitionKey = "partitionKey-1";        
            var result = _amazonKinesisClient.PutRecordAsync(requestRecord).Result;
            return Task.FromResult(result);            
        }

        private Byte[] createRecord(IDomainEvent @event)
        {
            return Encoding.UTF8.GetBytes(
                JsonConvert.SerializeObject(
                    new EventRecord(
                        @event.GetType().ToString(),      
                        Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(@event)), 
                        DateTime.Now)
                    ));
        }
            
        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

    }
}
