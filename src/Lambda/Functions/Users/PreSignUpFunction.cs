using Amazon.Lambda.Core;
using Cloudbash.Application.Users.Commands.CreateUser;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace Cloudbash.Lambda.Functions.Users
{
    public class PreSignUpFunction : CognitoTriggerFunctionBase
    {

        [LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
        public async Task<JObject> Run(JObject input, ILambdaContext context) {
            return input;
        }
    }
}
