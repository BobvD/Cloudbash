
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Cloudbash.Application.Carts.Commands.CreateCart;
using Cloudbash.Application.Carts.Queries.GetCart;
using Cloudbash.Domain.ReadModels;
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
                var cartId = await Mediator.Send(new CreateCartCommand { CustomerId = id });
                result = new Cart { CustomerId = id, Id = cartId.ToString() };
            }

            return GenerateResponse(200, result);
        }

        private async Task<object> GetCart(Guid id)
        {
            return await Mediator.Send(new GetCartQuery { CustomerId = id }); 
        }
    }
}