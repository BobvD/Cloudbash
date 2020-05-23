using Newtonsoft.Json.Linq;

namespace Cloudbash.Lambda.Functions.Users
{
    public abstract class CognitoTriggerFunctionBase : FunctionBase
    {
        public string GetUserAttribute(JObject input, string key)
        {
            return (string)input["request"]["userAttributes"][key];
        }
    }
}
