﻿using Amazon.SQS;
using Amazon.SQS.Model;
using Cloudbash.Domain.SeedWork;
using Cloudbash.Infrastructure.Configs;
using System.Threading.Tasks;

namespace Cloudbash.Infrastructure.EventStream
{
    public class SQSEventStream : EventStreamBase
    {

        
        private readonly AmazonSQSClient _amazonSQSClient;
        private readonly IServerlessConfiguration _config;

        public SQSEventStream(IAwsClientFactory<AmazonSQSClient> clientFactory, IServerlessConfiguration config)
        {
            _amazonSQSClient = clientFactory.GetAwsClient();
            _config = config;
        }
        

        public override async Task PublishAsync(IDomainEvent @event)
        {                                   
            var sendMessageRequest = new SendMessageRequest
            {
                MessageBody = CreateEnveloppe(@event),
                QueueUrl = _config.SQSUrl
            };

            await _amazonSQSClient.SendMessageAsync(sendMessageRequest);            
        }

    }
}
