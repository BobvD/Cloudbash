﻿using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Amazon.Runtime.Internal.Transform;
using Cloudbash.Application.Concerts.Queries.GetConcerts;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cloudbash.Lambda.Functions.Concerts
{
    public class GetConcertsFunction : FunctionBase
    {

        [LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
        public async Task<APIGatewayProxyResponse> Run()
        {                   
            var result = await Mediator.Send(new GetConcertsQuery());

            return new APIGatewayProxyResponse
            {
                Headers = GetCorsHeaders(),
                StatusCode = 201,
                Body = JsonConvert.SerializeObject(result)
            };
        }
        
    }
}