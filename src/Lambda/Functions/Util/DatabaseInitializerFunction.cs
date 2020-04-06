
using Amazon.Lambda.Core;
using Cloudbash.Infrastructure.Persistence;
using System;
using Microsoft.Extensions.DependencyInjection;

namespace Cloudbash.Lambda.Functions.Util
{
    public class DatabaseInitializerFunction : FunctionBase
    {        
        [LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
        public void Run()
        {            
            // Make sure database exists
            try
            {
                using (var context = _serviceProvider.GetService<ApplicationDbContext>())
                {
                    Console.WriteLine("Try to init database...");
                    context.Database.EnsureCreated();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to init database");
                Console.WriteLine("Error message: " + e.Message);
            }
        }
    }
}
