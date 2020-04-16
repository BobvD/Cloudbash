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

            // Deserialize the request body to the correct command.
            var requestModel = JsonConvert.DeserializeObject<CreateConcertCommand>(request.Body);

            // Fix needed: make the error-handling middleware work. 
            // For now: catch the error from Mediatr and return the correct API Gateway Response
            try
            {
                // Send the Command to the correct Handler with Mediator
                var result = await Mediator.Send(requestModel);
                // Return an API Gateway result with the request as body (serialized to json)
                return new APIGatewayProxyResponse { StatusCode = 201, Body = JsonConvert.SerializeObject(result) };
            }
            catch (Exception ex)
            {
                // On bad request return status 400 with the errors as body
                return new APIGatewayProxyResponse { StatusCode = 400, Body = JsonConvert.SerializeObject(ex.Message) };
            }             
            
        }
    }
}