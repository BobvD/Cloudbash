using Amazon.SQS;
using Amazon.SQS.Model;
using Cloudbash.Application.Common.Interfaces;
using Cloudbash.Domain.SeedWork;
using Cloudbash.Infrastructure.Configs;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cloudbash.Infrastructure.EventStream
{
    public class SQSEventStream : IPublisher
    {

        
        private readonly AmazonSQSClient _amazonSQSClient;
        private readonly IServerlessConfiguration _config;

        public SQSEventStream(IAwsClientFactory<AmazonSQSClient> clientFactory, IServerlessConfiguration config)
        {
            _amazonSQSClient = clientFactory.GetAwsClient();
            _config = config;
        }
        

        public async Task PublishAsync(IDomainEvent domainEvent)
        {           
            
            var sendMessageRequest = new SendMessageRequest
            {
                DelaySeconds = 10,
                MessageAttributes = new Dictionary<string, MessageAttributeValue>
                {
                    {"Type",   new MessageAttributeValue{DataType = "String", StringValue = domainEvent.GetType().ToString() }}
                },
                MessageBody = Serialize(domainEvent),
                QueueUrl = _config.SQSUrl
            };

            await _amazonSQSClient.SendMessageAsync(sendMessageRequest);
            
        }

        private string Serialize(IDomainEvent @event)
        {
            return JsonConvert.SerializeObject(@event);
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }
    }
}
