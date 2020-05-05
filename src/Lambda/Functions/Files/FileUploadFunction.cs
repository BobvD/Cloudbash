using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;

namespace Cloudbash.Lambda.Functions.Files
{
    public class FileUploadFunction : FunctionBase
    {

        [LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
        public async Task<APIGatewayProxyResponse> Run(JObject input, ILambdaContext context)
        {
            Console.WriteLine(input.ToString());

            return new APIGatewayProxyResponse
            {
                Headers = GetCorsHeaders(),
                StatusCode = 201,
                Body = "test test"
            };
        }


    }
}
