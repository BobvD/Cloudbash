using Amazon.Lambda.Core;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace Cloudbash.Lambda.Functions.Users
{
    public class PostConfirmationFunction : FunctionBase
    {
        [LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
        public async Task<JObject> Run(JObject input, ILambdaContext context)
        {
            context.Logger.LogLine("User email confirmed: " + input);
            return input;
        }
    }
}
