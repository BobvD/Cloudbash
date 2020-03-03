using Amazon.Lambda.Core;
using Cloudbash.Lambda.Functions;

namespace Cloudbash.Lambda.Events.Functions
{
    class ProcessEventsFunction : FunctionBase
    {

        [LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
        public void Run(ILambdaContext context)
        {
            context.Logger.Log("Process event invoked");
        }
    }
}
