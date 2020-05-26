using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Cloudbash.Application.Concerts.Commands.DeleteConcert;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;


namespace Cloudbash.Lambda.Functions.Concerts
{
    public class DeleteConcertFunction : FunctionBase
    {

        [LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
        public async Task<APIGatewayProxyResponse> Run(APIGatewayProxyRequest request)
        {
            try
            {
                Guid id = Guid.Parse(GetPathParameter(request, "id"));
                var result = await Mediator.Send(new DeleteConcertCommand { Id = id } );

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