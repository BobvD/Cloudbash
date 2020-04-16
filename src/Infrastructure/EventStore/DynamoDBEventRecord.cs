using Amazon.DynamoDBv2.DataModel;
using Cloudbash.Domain.EventStore;
using Cloudbash.Domain.SeedWork;
using System;

namespace Cloudbash.Infrastructure.Persistence.EventStore
{
    [DynamoDBTable("EventLog")]
    public class DynamoDBEventRecord : IEventRecord
    {
        [DynamoDBHashKey]
        public Guid AggregateId { get; set; }
        [DynamoDBProperty]
        public string EventType { get; set; }
        [DynamoDBProperty]
        public string Data { get; set; }
        [DynamoDBRangeKey]
        public long AggregateVersion { get; set; }
        [DynamoDBProperty]
        public DateTime Created { get; set; }

        public DynamoDBEventRecord() { }

        public DynamoDBEventRecord(EventRecord eventRecord)
        {
            AggregateId = eventRecord.AggregateId;
            EventType = eventRecord.EventType;
            AggregateVersion = eventRecord.AggregateVersion;
            Data = eventRecord.Data;
            Created = eventRecord.Created;
        }
    }
}
