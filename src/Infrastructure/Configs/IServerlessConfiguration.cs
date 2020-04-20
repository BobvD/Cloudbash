using System;
using System.Collections.Generic;
using System.Text;

namespace Cloudbash.Infrastructure.Configs
{
    public interface IServerlessConfiguration
    {
        EventBusType EventBus { get; }
        string SQSUrl { get;  }
        string RedisConnectionString { get; }
    }

    public class ServerlessConfiguration : IServerlessConfiguration
    {
        public EventBusType EventBus { get; set; }
        public string SQSUrl { get; set; }
        public string RedisConnectionString { get; set; }
    }

    public enum EventBusType
    {
        SQS,
        KINESIS 
    }
}
