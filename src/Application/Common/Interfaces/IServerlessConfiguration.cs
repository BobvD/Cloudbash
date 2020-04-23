namespace Cloudbash.Infrastructure.Configs
{
    public interface IServerlessConfiguration
    {
        EventBusType EventBus { get; }
        DatabaseType Database { get; }
        string EventStoreTableName { get; }
        string KinesisStreamName { get; }
        string KinesisPartitionKey { get; }
        string SQSUrl { get;  }
        RedisConfiguration Redis { get; set; }
        string RedisConnectionString { get; }
    }

    public class ServerlessConfiguration : IServerlessConfiguration
    {
        public EventBusType EventBus { get; set; }
        public DatabaseType Database { get; set; }
        public string SQSUrl { get; set; }
        public string KinesisStreamName { get; set; }
        public string KinesisPartitionKey { get; set; } = "partitionKey-1";
        public string EventStoreTableName { get; set; }
        public RedisConfiguration Redis { get; set; }
        public string RedisConnectionString { get { return $"{Redis.Host}:{Redis.Port}"; } }
    }

    public enum EventBusType
    {
        SQS,
        KINESIS 
    }

    public enum DatabaseType
    {
        POSTGRES,
        REDIS
    }

    public class RedisConfiguration
    {
        public int Port { get; set; }
        public string Host { get; set; }
    }
}
