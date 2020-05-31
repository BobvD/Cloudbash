using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Cloudbash.Application.Concerts.Commands.ScheduleConcert;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Cloudbash.Lambda.Functions.Concerts
{
    public class ScheduleConcertFunction : FunctionBase
    {

        [LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
        public async Task<APIGatewayProxyResponse> Run(APIGatewayProxyRequest request)
        {
            try
            {
                Guid concertId = Guid.Parse(GetPathParameter(request, "id"));                
                var requestModel = JsonConvert.DeserializeObject<ScheduleConcertCommand>(request.Body);
                requestModel.ConcertId = concertId;

                var result = await Mediator.Send(requestModel);

                return GenerateResponse(201, result);
            }
            catch (Exception ex)
            {
                LambdaLogger.Log(ex.Message);
                return GenerateResponse(400, ex.Message);
            }


        }
    }
}