using Amazon.DynamoDBv2.DataModel;
using Cloudbash.Domain.SeedWork;
using System;

namespace Cloudbash.Infrastructure.Persistence.EventStore
{
    [DynamoDBTable("EventLog")]
    public class DynamoDBEventRecord
    {
        [DynamoDBHashKey]
        public string Id { get; set; }
        [DynamoDBProperty]
        public string EventType { get; set; }
        [DynamoDBProperty]
        public string Data { get; set; }
        [DynamoDBProperty]
        public long EventVersion { get; set; }
        [DynamoDBProperty]
        public DateTime Created { get; set; }

        public DynamoDBEventRecord() { }

        public DynamoDBEventRecord(EventRecord eventRecord)
        {
            Id = eventRecord.AggregateId.ToString();
            EventType = eventRecord.EventType;
            EventVersion = eventRecord.EventVersion;
            Data = eventRecord.Data;
            Created = eventRecord.Created;
        }
    }
}
