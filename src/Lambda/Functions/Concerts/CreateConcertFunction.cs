using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Cloudbash.Application.Concerts.Commands.CreateConcert;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Cloudbash.Lambda.Functions.Concerts
{
    public class CreateConcertFunction : FunctionBase
    {
        
        [LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
        public async Task<APIGatewayProxyResponse> Run(APIGatewayProxyRequest request)
        {
            var requestModel = JsonConvert.DeserializeObject<CreateConcertCommand>(request.Body); 
            var result = await Mediator.Send(requestModel);
            return new APIGatewayProxyResponse { StatusCode = 201, Body = JsonConvert.SerializeObject(result) };
        }
    }
}