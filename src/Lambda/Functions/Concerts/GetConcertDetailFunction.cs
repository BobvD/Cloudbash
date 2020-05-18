using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Cloudbash.Application.Concerts.Queries.GetConcert;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Cloudbash.Lambda.Functions.Concerts
{
    public class GetConcertDetailFunction : FunctionBase
    {

        [LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
        public async Task<APIGatewayProxyResponse> Run(APIGatewayProxyRequest request)
        {
            string id;
            if (!request.PathParameters.TryGetValue("id", out id))
                return new APIGatewayProxyResponse { StatusCode = (int)HttpStatusCode.InternalServerError };

            var result = await Mediator.Send(new GetConcertDetailQuery { Id = new Guid(id) });;

            return new APIGatewayProxyResponse
            {
                Headers = GetCorsHeaders(),
                StatusCode = 201,
                Body = JsonConvert.SerializeObject(result)
            };
        }

    }
}