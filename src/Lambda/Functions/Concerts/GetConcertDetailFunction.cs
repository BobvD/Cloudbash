using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Cloudbash.Application.Concerts.Queries.GetConcert;
using System;
using System.Threading.Tasks;

namespace Cloudbash.Lambda.Functions.Concerts
{
    public class GetConcertDetailFunction : FunctionBase
    {

        [LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
        public async Task<APIGatewayProxyResponse> Run(APIGatewayProxyRequest request)
        {
            Guid id = Guid.Parse(GetPathParameter(request, "id"));
            var result = await Mediator.Send(new GetConcertDetailQuery { Id = id });

            return GenerateResponse(200, result);
        }

    }
}