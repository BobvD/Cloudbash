using Amazon.Lambda.Core;
using Cloudbash.Application.Users.Commands.CreateUser;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace Cloudbash.Lambda.Functions.Users
{
    public class PostConfirmationFunction : FunctionBase
    {
        [LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
        public async Task<JObject> Run(JObject input, ILambdaContext context)
        {
            
            try
            {
                var attributes = input["request"]["userAttributes"];

                var userData = new CreateUserCommand
                {
                    Id = new System.Guid((string)attributes["sub"]),
                    FullName = (string)attributes["name"],
                    Email = (string)attributes["email"]
                };

                var result = await Mediator.Send(userData);
            }
            catch (System.Exception)
            {

                throw;
            }

            return input;
        }
    }
}
