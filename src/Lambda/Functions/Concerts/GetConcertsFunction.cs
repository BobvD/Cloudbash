using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Cloudbash.Application.Concerts.Queries.GetConcerts;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Cloudbash.Lambda.Functions.Concerts
{
    public class GetConcertsFunction : FunctionBase
    {

        [LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
        public async Task<APIGatewayProxyResponse> Run()
        {                   
            var result = await Mediator.Send(new GetConcertsQuery());
            return new APIGatewayProxyResponse { StatusCode = 201, Body = JsonConvert.SerializeObject(result) };                       
        }
    }
}