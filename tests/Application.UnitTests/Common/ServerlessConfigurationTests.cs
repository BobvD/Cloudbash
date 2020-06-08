using Cloudbash.Infrastructure.Configs;
using Shouldly;
using Xunit;

namespace Cloudbash.Application.UnitTests.Common
{
    public class ServerlessConfigurationTests
    {

        [Fact]
        public void Test_Dynamo_Config()
        {
            var config = new ServerlessConfiguration
            {
                ConfigName = "Config #1",
                EventBus = EventBusType.DYNAMO,
                Database = DatabaseType.DYNAMO,
                EventStoreTableName = "ES-1",
                BucketName = "ES-1"
            };

            config.ConfigName.ShouldBe("Config #1");
            config.EventBus.ShouldBe(EventBusType.DYNAMO);
            config.Database.ShouldBe(DatabaseType.DYNAMO);
            config.EventStoreTableName.ShouldBe("ES-1");
            config.BucketName.ShouldBe("ES-1");

        }

        [Fact]
        public void Test_Kinesis_Config()
        {

            var config2 = new ServerlessConfiguration
            {
                ConfigName = "Config #2",
                EventBus = EventBusType.KINESIS,
                Database = DatabaseType.POSTGRES,
                EventStoreTableName = "ES-2",
                BucketName = "ES-2",
                KinesisStreamName = "ES-2",
                KinesisPartitionKey = "ES-2"
            };

            config2.ConfigName.ShouldBe("Config #2");
            config2.EventBus.ShouldBe(EventBusType.KINESIS);
            config2.Database.ShouldBe(DatabaseType.POSTGRES);
            config2.EventStoreTableName.ShouldBe("ES-2");
            config2.BucketName.ShouldBe("ES-2");
            config2.KinesisPartitionKey.ShouldBe("ES-2");
            config2.KinesisStreamName.ShouldBe("ES-2");
        }

        [Fact]
        public void Test_SQS_REDIS_Config()
        {

            var config3 = new ServerlessConfiguration
            {
                ConfigName = "Config #3",
                EventBus = EventBusType.SQS,
                Database = DatabaseType.REDIS,
                EventStoreTableName = "ES-3",
                BucketName = "ES-3",
                SQSUrl = "ES-3",
                Redis = new RedisConfiguration
                {
                    Host = "HOST",
                    Port = 55
                }
            };

            config3.ConfigName.ShouldBe("Config #3");
            config3.EventBus.ShouldBe(EventBusType.SQS);
            config3.Database.ShouldBe(DatabaseType.REDIS);
            config3.EventStoreTableName.ShouldBe("ES-3");
            config3.BucketName.ShouldBe("ES-3");
            config3.SQSUrl.ShouldBe("ES-3");
            config3.Redis.Host.ShouldBe("HOST");
            config3.Redis.Port.ShouldBe(55);
            config3.RedisConnectionString.ShouldBe("HOST:55");

        }

    }
}
