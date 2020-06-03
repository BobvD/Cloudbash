using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Cloudbash.Application.Carts.Commands.CheckOutCart;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Cloudbash.Lambda.Functions.Carts
{
    public class CheckOutCartFunction : FunctionBase
    {

        [LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
        public async Task<APIGatewayProxyResponse> Run(APIGatewayProxyRequest request)
        {
            var command = JsonConvert.DeserializeObject<CheckOutCartCommand>(request.Body);
            var result = await Mediator.Send(command);

            return GenerateResponse(204, result);
        }
    }
}
