
using Amazon.Lambda.Core;
using System;

namespace Cloudbash.Lambda.Functions.Util
{
    public class DatabaseInitializerFunction : FunctionBase
    {        
        [LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
        public void Run()
        {
            Console.WriteLine("Init database...");
        }
    }
}
