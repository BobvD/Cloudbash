using Amazon.DynamoDBv2.DataModel;
using Cloudbash.Domain.SeedWork;
using System;

namespace Cloudbash.Infrastructure.Persistence
{
    [DynamoDBTable("EventLog")]
    public class DynamoDBEventRecord
    {
        [DynamoDBHashKey]
        public string Id { get; set; }
        [DynamoDBProperty]
        public string EventType { get; set; }
        [DynamoDBProperty]
        public byte[] Data { get; set; }
        [DynamoDBProperty]
        public DateTime Created { get; set; }

        public DynamoDBEventRecord() { }

        public DynamoDBEventRecord(EventRecord eventRecord)
        {
            Id = Guid.NewGuid().ToString();
            EventType = eventRecord.EventType;
            Data = eventRecord.Data;
            Created = eventRecord.Created;
        }
    }
}
