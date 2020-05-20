using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Cloudbash.Application.Concerts.Commands.ScheduleConcert;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Cloudbash.Lambda.Functions.Concerts
{
    public class ScheduleConcertFunction : FunctionBase
    {

        [LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
        public async Task<APIGatewayProxyResponse> Run(APIGatewayProxyRequest request)
        {
            try
            {
                Console.WriteLine(request.Body);
                Guid concertId = Guid.Parse(GetPathParameter(request, "id"));
                var requestModel = JsonConvert.DeserializeObject<ScheduleConcertCommand>(request.Body);
                requestModel.ConcertId = concertId;

                var result = await Mediator.Send(requestModel);

                return new APIGatewayProxyResponse
                {
                    Headers = GetCorsHeaders(),
                    StatusCode = 201,
                    Body = JsonConvert.SerializeObject(result)
                };
            }
            catch (Exception ex)
            {
                return new APIGatewayProxyResponse
                {
                    Headers = GetCorsHeaders(),
                    StatusCode = 400,
                    Body = JsonConvert.SerializeObject(ex.Message)
                };
            }


        }
    }
}