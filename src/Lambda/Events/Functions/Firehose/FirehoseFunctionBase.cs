using Cloudbash.Infrastructure.Firehose;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using Serializer = Amazon.Lambda.Serialization.Json.JsonSerializer;

namespace Cloudbash.Lambda.Events.Functions.Firehose
{
    public abstract class FirehoseFunctionBase
    {
        protected IServiceProvider _serviceProvider;
        protected IFirehoseClient _client;
        private static readonly Serializer _jsonSerializer = new Serializer();

        protected FirehoseFunctionBase() : this(Startup
          .BuildContainer()
          .BuildServiceProvider())
        {
        }

        protected FirehoseFunctionBase(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _client = _serviceProvider.GetService<IFirehoseClient>();
        }

        protected static byte[] SerializeObject(object streamRecord)
        {
            using (var ms = new MemoryStream())
            {
                _jsonSerializer.Serialize(streamRecord, ms);
                return ms.ToArray();
            }
        }
    }
}
