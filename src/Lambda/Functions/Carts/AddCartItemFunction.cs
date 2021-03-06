﻿using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Cloudbash.Application.Carts.Commands.AddCartItem;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Cloudbash.Lambda.Functions.Carts
{
    public class AddCartItemFunction : FunctionBase
    {

        [LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
        public async Task<APIGatewayProxyResponse> Run(APIGatewayProxyRequest request)
        {
            var command = JsonConvert.DeserializeObject<AddCartItemCommand>(request.Body);
            var result = await Mediator.Send(command);

            return GenerateResponse(201, result);            
        }
    }
}