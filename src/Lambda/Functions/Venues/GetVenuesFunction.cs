﻿using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Cloudbash.Application.Venues.Queries.GetVenues;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Cloudbash.Lambda.Functions.Venues
{
    public class GetVenuesFunction : FunctionBase
    {

        [LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
        public async Task<APIGatewayProxyResponse> Run()
        {
            var result = await Mediator.Send(new GetVenuesQuery());

            return new APIGatewayProxyResponse
            {
                Headers = GetCorsHeaders(),
                StatusCode = 200,
                Body = JsonConvert.SerializeObject(result)
            };
        }

    }
}