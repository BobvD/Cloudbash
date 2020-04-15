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

            // Save the Aggregate ID
            JObject userAttributes = input["request"]["userAttributes"] as JObject;
            userAttributes.Add("custom:aggregate_id", result.ToString());
            userAttributes.Remove("phone_number");
            userAttributes.Add("phone_number", "+1213123123");
            userAttributes.Add("custom:id", "testetsttest");
            input["request"]["userAttributes"] = userAttributes;

            
            context.Logger.LogLine("New user signup: " + result);
            context.Logger.LogLine("User: " + input);
            return input;
        }
    }
}
