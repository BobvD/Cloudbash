using Amazon.ApiGatewayManagementApi;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using System;
using System.Collections.Generic;

namespace Cloudbash.Lambda.Functions.Util
{
    public class EventStoreWebSocketFunction : FunctionBase
    {
        Func<string, IAmazonApiGatewayManagementApi> _apiGatewayMangementApiClientFactory;
        public EventStoreWebSocketFunction() : base()
        {
            _apiGatewayMangementApiClientFactory = (Func<string, AmazonApiGatewayManagementApiClient>)((endpoint) =>
            {
                return new AmazonApiGatewayManagementApiClient(new AmazonApiGatewayManagementApiConfig
                {
                    ServiceURL = endpoint
                });
            });
        }

        [LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
        public APIGatewayProxyResponse Run(APIGatewayProxyRequest request, ILambdaContext context)
        {

            var domainName = request.RequestContext.DomainName;
            var stage = request.RequestContext.Stage;
            var endpoint = $"https://{domainName}/{stage}";

            var routeKey = request.RequestContext.RouteKey;
            Console.WriteLine("Route key:" + routeKey);

            

            var apiClient = _apiGatewayMangementApiClientFactory(endpoint);

            return new APIGatewayProxyResponse
            {
                StatusCode = 200,
                Body = "Connected."
            };
            /*
            switch (routeKey)
            {
                case "$connect":
                    return new APIGatewayProxyResponse
                    {
                        StatusCode = 200,
                        Body = "Connected."
                    };
                case "$disconnect":
                    return new APIGatewayProxyResponse
                    {
                        StatusCode = 200,
                        Body = "Disconnected"

                    };
                case "routeA":
                    return new APIGatewayProxyResponse
                    {
                        StatusCode = 200,
                        Body = "Connected."
                    };
                case "$default":
                    Console.WriteLine("ECHO MESSAGE");
                    return new APIGatewayProxyResponse
                    {
                        StatusCode = 200,
                        Body = "Connected."
                    };
                default:
                    break;
            }

            return new APIGatewayProxyResponse
            {
                StatusCode = 200,
                Body = "Connected."
            };
            */
        }

        public Dictionary<string, string>  GetHeaders()
        {
            return new Dictionary<string, string> { { "Content-Type", "text/plain" } };
        }
    }
}
