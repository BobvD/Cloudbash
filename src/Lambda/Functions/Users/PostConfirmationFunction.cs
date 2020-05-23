using Amazon.Lambda.Core;
using Cloudbash.Application.Users.Commands.CreateUser;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace Cloudbash.Lambda.Functions.Users
{
    public class PostConfirmationFunction : CognitoTriggerFunctionBase
    {
        [LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
        public async Task<JObject> Run(JObject input, ILambdaContext context)
        {                    
            var userData = new CreateUserCommand
            {
                Id = new System.Guid(GetUserAttribute(input, "sub")),
                FullName = GetUserAttribute(input, "name"),
                Email = GetUserAttribute(input, "email")
            };

            await Mediator.Send(userData);           

            return input;
        }
    }
}
