using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Cloudbash.Application.Concerts.Queries.FilterConcerts;
using System;
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
            
            DateTime before = new DateTime();
            DateTime.TryParse(GetQueryParameter(request, "before"), out before);
            command.After = before;

            DateTime after = new DateTime();
            DateTime.TryParse(GetQueryParameter(request, "after"), out after);
            command.After = after;

            var result = await Mediator.Send(command);
            return GenerateResponse(200, result);
        }

    }
}
