using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Cloudbash.Application.Concerts.Commands.CreateTicketType;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Cloudbash.Lambda.Functions.Concerts
{
    public class CreateTicketTypeFunction : FunctionBase
    {

        [LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
        public async Task<APIGatewayProxyResponse> Run(APIGatewayProxyRequest request)
        {
           
            string id;
            if (!request.PathParameters.TryGetValue("id", out id))
                return new APIGatewayProxyResponse { StatusCode = (int)HttpStatusCode.InternalServerError };
          
            var requestModel = JsonConvert.DeserializeObject<CreateTicketTypeCommand>(request.Body);
            
            try
            {
                requestModel.ConcertId = new Guid(id);
                var result = await Mediator.Send(requestModel);
                return new APIGatewayProxyResponse
                {
                    Headers = GetCorsHeaders(),
                    StatusCode = 201,
                    Body = JsonConvert.SerializeObject(result)
                };
            }
            catch (Exception ex)
            {
                return new APIGatewayProxyResponse
                {
                    Headers = GetCorsHeaders(),
                    StatusCode = 400,
                    Body = JsonConvert.SerializeObject(ex.Message)
                };
            }
        }
    }
}