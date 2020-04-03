
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Cloudbash.Domain.SeedWork;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Cloudbash.Infrastructure.Persistence.EventStore
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

        public async Task SaveAsync(IDomainEvent @event)
        {
            using (var context = new DynamoDBContext(_amazonDynamoDBClient))
            {
                Console.WriteLine("Start saving new item");
                var item = new DynamoDBEventRecord(
                                new EventRecord(@event.AggregateId, 
                                @event.GetType().ToString(), 
                                JsonConvert.SerializeObject(@event), 
                                @event.AggregateVersion, 
                                new DateTime()));
                Console.WriteLine(item.ToString());
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

    }

    

}
