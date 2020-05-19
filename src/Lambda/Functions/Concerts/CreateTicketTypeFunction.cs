using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Cloudbash.Application.Concerts.Commands.CreateTicketType;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Cloudbash.Lambda.Functions.Concerts
{
    public class CreateTicketTypeFunction : FunctionBase
    {

        [LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
        public async Task<APIGatewayProxyResponse> Run(APIGatewayProxyRequest request)
        {            
            try
            {
                Guid concertId = Guid.Parse(GetPathParameter(request, "id"));
                var requestModel = JsonConvert.DeserializeObject<CreateTicketTypeCommand>(request.Body);
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