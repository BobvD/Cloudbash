using Amazon.Lambda.APIGatewayEvents;
using Amazon.Runtime.Internal.Transform;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace Cloudbash.Lambda.Functions
{
    public abstract class FunctionBase
    {
        protected IServiceProvider _serviceProvider;
        protected IMediator Mediator;

        protected FunctionBase() : this(Startup
          .BuildContainer()
          .AddLogging( a=> {
              a.AddConsole();
          })
          .BuildServiceProvider())
        {
        }

        protected FunctionBase(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            Mediator = _serviceProvider.GetService<IMediator>();
        }

        protected static IDictionary<string, string> GetCorsHeaders()
        {
            return new Dictionary<string, string>
            {
                new KeyValuePair<string, string>("Access-Control-Allow-Origin", "*"),
                new KeyValuePair<string, string>("Access-Control-Allow-Credentials", "true"),
                new KeyValuePair<string, string>("Access-Control-Allow-Methods", "*")
            };
        }

        protected static string GetPathParameter(APIGatewayProxyRequest request, string key)
        {
            if (request.PathParameters == null || !request.PathParameters.ContainsKey(key))
            {
                throw new ArgumentNullException();
            }

            string value;

            if (!request.PathParameters.TryGetValue(key, out value))
            {
                throw new ArgumentNullException();
            }

            return value;
        }
    }

}

