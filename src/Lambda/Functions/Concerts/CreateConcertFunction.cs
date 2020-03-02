using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Cloudbash.Application.Concerts.Commands.CreateConcert;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Cloudbash.Lambda.Functions.Concerts
{
    public class CreateConcertFunction
    {
        private readonly IServiceProvider _serviceProvider;

        public CreateConcertFunction() : this(Startup
          .BuildContainer()
          .BuildServiceProvider())
        {
        }

        public CreateConcertFunction(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        [LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
        public async Task<APIGatewayProxyResponse> Run(APIGatewayProxyRequest request)
        {
            var requestModel = JsonConvert.DeserializeObject<CreateConcertCommand>(request.Body);
            var mediator = _serviceProvider.GetService<IMediator>();

            var result = await mediator.Send(requestModel);

            return new APIGatewayProxyResponse { StatusCode = 201, Body = JsonConvert.SerializeObject(result) };
        }
    }
}