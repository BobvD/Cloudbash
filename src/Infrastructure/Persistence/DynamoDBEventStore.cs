
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.Kinesis;
using Cloudbash.Domain.SeedWork;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Cloudbash.Infrastructure.Persistence
{
    public class DynamoDBEventStore : IEventStore
    {
        private readonly AmazonDynamoDBClient _amazonKinesisClient;
        private readonly Table _table;

        public DynamoDBEventStore(IAwsClientFactory<AmazonDynamoDBClient> clientFactory)
        {
            _amazonKinesisClient = clientFactory.GetAwsClient();
            _table = Table.LoadTable(_amazonKinesisClient, "eventlog");
        }

        public Task<IEnumerable<IDomainEvent>> GetAsync(string aggregateId, string aggregateType, int fromVersion)
        {
            throw new System.NotImplementedException();
        }

        public async Task SaveAsync(EventRecord @event, CancellationToken cancellationToken = default)
        {
            string json = JsonConvert.SerializeObject(@event);
            var item = Document.FromJson(json);
            await _table.PutItemAsync(item, cancellationToken).ConfigureAwait(false);
        }
    }
}
