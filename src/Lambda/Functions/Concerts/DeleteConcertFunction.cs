using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Amazon.Runtime.Internal.Transform;
using Cloudbash.Application.Concerts.Commands.DeleteConcert;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;


namespace Cloudbash.Lambda.Functions.Concerts
{
    public class DeleteConcertFunction : FunctionBase
    {

        [LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
        public async Task<APIGatewayProxyResponse> Run(APIGatewayProxyRequest request)
        {
            if (request.PathParameters == null || !request.PathParameters.ContainsKey("id"))
                return new APIGatewayProxyResponse { StatusCode = (int)HttpStatusCode.InternalServerError };
            
            string id;
            if (!request.PathParameters.TryGetValue("id", out id))
                return new APIGatewayProxyResponse { StatusCode = (int)HttpStatusCode.InternalServerError };

            Console.WriteLine("REQUEST TO DELETE CONCERT: " + id);
            try
            {
                var result = await Mediator.Send(new DeleteConcertCommand { Id = Guid.Parse(id) } );

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