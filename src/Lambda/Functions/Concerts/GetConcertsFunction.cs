using Amazon.Lambda.APIGatewayEvents;
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

        private IDictionary<string, string> GetCorsHeaders()
        {
            return new Dictionary<string, string>()
            {
                new KeyValuePair<string, string>("Access-Control-Allow-Origin", "*"),
                new KeyValuePair<string, string>("Access-Control-Allow-Credentials", "true"),
                new KeyValuePair<string, string>("Access-Control-Allow-Methods", "OPTIONS,POST,GET")
            };
        }

    }
}