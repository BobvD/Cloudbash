
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Cloudbash.Application.Carts.Commands.CreateCart;
using Cloudbash.Application.Carts.Queries.GetCart;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Cloudbash.Lambda.Functions.Carts
{
    public class GetCartFunction : FunctionBase
    {

        [LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
        public async Task<APIGatewayProxyResponse> Run(APIGatewayProxyRequest request)
        {
            Guid id = Guid.Parse(GetPathParameter(request, "customerId"));

            var result = await GetCart(id);

            if(result == null)
            {
                await Mediator.Send(new CreateCartCommand { CustomerId = id });
                result = await GetCart(id);
            }

            return new APIGatewayProxyResponse
            {
                Headers = GetCorsHeaders(),
                StatusCode = 201,
                Body = JsonConvert.SerializeObject(result)
            };
        }

        private async Task<object> GetCart(Guid id)
        {
            return await Mediator.Send(new GetCartQuery { CustomerId = id }); 
        }
    }
}