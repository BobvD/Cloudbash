using Cloudbash.Infrastructure.Firehose;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Cloudbash.Lambda.Events.Functions.Firehose
{
    public abstract class FirehoseFunction
    {
        public IServiceProvider _serviceProvider;
        protected IFirehoseClient _client;

        public FirehoseFunction() : this(Startup
          .BuildContainer()
          .BuildServiceProvider())
        {
        }

        public FirehoseFunction(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _client = _serviceProvider.GetService<IFirehoseClient>();
        }
    }
}
