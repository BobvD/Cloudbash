
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Cloudbash.Domain.SeedWork;
using Cloudbash.Infrastructure.EventStore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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

        public async Task<IEnumerable<IDomainEvent>> GetAsync(Guid aggregateId)
        {
           
            var events = new List<IDomainEvent>();

            using (var context = new DynamoDBContext(_amazonDynamoDBClient))
            {               
                // string id = aggregateId.ToString();
                AsyncSearch<DynamoDBEventRecord> recordQuery = context.QueryAsync<DynamoDBEventRecord>(aggregateId, _configuration);
                
                Task<List<DynamoDBEventRecord>> recordTask = recordQuery.GetRemainingAsync();
                recordTask.Wait();

                if (recordTask.Exception == null)
                 {
                    Console.WriteLine("count: " + recordTask.Result.Count);

                    foreach (var @event in recordTask.Result)
                    {
                        events.Add(Deserialize(@event.EventType, @event.Data));
                    }
                    return events;
                 }
            }

            return events;
        }

        public async Task SaveAsync(IDomainEvent @event)
        {
            using (var context = new DynamoDBContext(_amazonDynamoDBClient))
            {
                var item = new DynamoDBEventRecord(
                                new EventRecord(@event.AggregateId, 
                                @event.GetType().ToString(), 
                                Serialize(@event), 
                                @event.AggregateVersion));
                
                await context.SaveAsync(item, _configuration);                            
            }
        }


        private IDomainEvent Deserialize(string eventType, string data)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings { ContractResolver = new PrivateSetterContractResolver() };
            return (IDomainEvent)JsonConvert.DeserializeObject(data, Type.GetType(eventType + ", Cloudbash.Domain", true), settings);
        }

        private string Serialize(IDomainEvent @event)
        {
            return JsonConvert.SerializeObject(@event);
        }

    }

    

}
