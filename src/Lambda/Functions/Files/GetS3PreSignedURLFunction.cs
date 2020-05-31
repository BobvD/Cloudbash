
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Cloudbash.Application.Files.Commands.GenerateS3PreSignedUrl;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Cloudbash.Lambda.Functions.Files
{
    public class GetS3PreSignedURLFunction : FunctionBase
    {
        [LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
        public async Task<APIGatewayProxyResponse> Run(APIGatewayProxyRequest request)
        {           
            var requestModel = JsonConvert.DeserializeObject<GetS3PreSignedUrlCommand>(request.Body);
            
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
