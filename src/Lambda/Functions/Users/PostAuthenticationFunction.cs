
using Amazon.Lambda.Core;
using Cloudbash.Application.Users.Commands.AddUserActivityLog;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace Cloudbash.Lambda.Functions.Users
{
    public class PostAuthenticationFunction : CognitoTriggerFunctionBase
    {
        [LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
        public async Task<JObject> Run(JObject input, ILambdaContext context)
        {
            var userData = new AddUserActivityLogCommand
            {
                UserId = new System.Guid(GetUserAttribute(input, "sub")),
                ActivityType = Domain.Users.UserActivityType.AUTHENTICATION
            };

            await Mediator.Send(userData);

            return input;
        }
    }
}
