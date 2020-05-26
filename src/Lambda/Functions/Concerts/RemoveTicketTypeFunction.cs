using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Cloudbash.Application.Concerts.Commands.RemoveTicketType;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Cloudbash.Lambda.Functions.Concerts
{
    public class RemoveTicketTypeFunction : FunctionBase
    {

        [LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
        public async Task<APIGatewayProxyResponse> Run(APIGatewayProxyRequest request) { 
        
            try
            {
                Guid concertId = Guid.Parse(GetPathParameter(request, "id"));
                Guid ticketTypeId = Guid.Parse(GetPathParameter(request, "ticketTypeId"));

                var result = await Mediator.Send(
                    new RemoveTicketTypeCommand { 
                        ConcertId = concertId,
                        TicketTypeId = ticketTypeId
                    });

                return new APIGatewayProxyResponse
                {
                    Headers = GetCorsHeaders(),
                    StatusCode = 204
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