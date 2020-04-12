using Amazon.Lambda.Core;
using Cloudbash.Application.Users.Commands.CreateUser;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace Cloudbash.Lambda.Functions.Users
{
    public class PreSignUpFunction : FunctionBase
    {

        [LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
        public async Task<JObject> Run(JObject input, ILambdaContext context) {
           

            var userData = new CreateUserCommand
            {
                Username = (string) input["userName"],
                Email = (string) input["request"]["userAttributes"]["email"]
            };

            var result = await Mediator.Send(userData);

            context.Logger.LogLine("New user signup: " + result);

            return input;
        }
    }
}
