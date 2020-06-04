using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Cloudbash.Application.Concerts.Queries.FilterConcerts;
using System.Threading.Tasks;

namespace Cloudbash.Lambda.Functions.Concerts
{
    public class FilterConcertsFunction : FunctionBase
    {

        [LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
        public async Task<APIGatewayProxyResponse> Run(APIGatewayProxyRequest request)
        {            
            var command = new FilterConcertsQuery();
            command.SearchTerm = GetQueryParameter(request, "searchTerm");
            command.VenueName = GetQueryParameter(request, "venueName");

            var result = await Mediator.Send(command);
            return GenerateResponse(200, result);
        }

    }
}
