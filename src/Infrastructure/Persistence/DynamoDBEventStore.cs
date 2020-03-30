
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Cloudbash.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Cloudbash.Infrastructure.Persistence
{
    public class DynamoDBEventStore : IEventStore
    {
        private readonly AmazonDynamoDBClient _amazonDynamoDBClient;
        private readonly DynamoDBOperationConfig _configuration;
        
        public DynamoDBEventStore(IAwsClientFactory<AmazonDynamoDBClient> clientFactory)
        {
            _amazonDynamoDBClient = clientFactory.GetAwsClient();
            _configuration = new DynamoDBOperationConfig
            {
                OverrideTableName = "EventLog",
                SkipVersionCheck = true
            };
        }

        public Task<IEnumerable<IDomainEvent>> GetAsync(string aggregateId, string aggregateType, int fromVersion)
        {
            throw new System.NotImplementedException();
        }

        public async Task SaveAsync(EventRecord @event, CancellationToken cancellationToken = default)
        {
            using (var context = new DynamoDBContext(_amazonDynamoDBClient))
            {
                var item = new DynamoDBEventRecord(@event);
                try
                {
                    await context.SaveAsync(item, _configuration);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
               
            }
        }

        public async Task SaveAsync(string @event, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();           
        }
    }

    

}
