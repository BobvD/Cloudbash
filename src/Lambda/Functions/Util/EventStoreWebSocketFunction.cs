using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cloudbash.Lambda.Functions.Util
{
    public class EventStoreWebSocketFunction : FunctionBase
    {
        [LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
        public APIGatewayProxyResponse Run(APIGatewayProxyRequest request, ILambdaContext context)
        {
            var routeKey = request.RequestContext.RouteKey;
            Console.WriteLine("Route key:" + routeKey);
            switch (routeKey)
            {
                case "$connect":
                    return new APIGatewayProxyResponse
                    {
                        StatusCode = 200,
                        Body = "Connected.",
                        Headers = GetHeaders()
                    };
                case "$disconnect":
                    return new APIGatewayProxyResponse
                    {
                        StatusCode = 200,
                        Body = "Disconnected",
                        Headers = GetHeaders()

                    };
                case "routeA":
                    return new APIGatewayProxyResponse
                    {
                        StatusCode = 200,
                        Body = "Connected.",
                        Headers = GetHeaders()
                    };
                case "$default":
                    Console.WriteLine("ECHO MESSAGE");
                    return new APIGatewayProxyResponse
                    {
                        StatusCode = 200,
                        Body = request.Body,
                        Headers = GetHeaders()
                    };
                default:
                    break;
            }

            return new APIGatewayProxyResponse
            {
                StatusCode = 200,
                Body = "Connected.",
                Headers = GetHeaders()
            };
        }

        public Dictionary<string, string>  GetHeaders()
        {
            return new Dictionary<string, string> { { "Content-Type", "text/plain" } };
        }
    }
}
