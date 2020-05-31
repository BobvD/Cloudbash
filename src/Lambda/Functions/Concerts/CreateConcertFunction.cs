using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Cloudbash.Application.Concerts.Commands.CreateConcert;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;


namespace Cloudbash.Lambda.Functions.Concerts
{
    public class CreateConcertFunction : FunctionBase
    {
        
        [LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
        public async Task<APIGatewayProxyResponse> Run(APIGatewayProxyRequest request)
        {
            var requestModel = JsonConvert.DeserializeObject<CreateConcertCommand>(request.Body);

            try
            {
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