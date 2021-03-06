﻿
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
            try
            {
                using (var context = _serviceProvider.GetService<ApplicationDbContext>())
                {
                    LambdaLogger.Log("Try to initialize database.");
                    context.Database.EnsureCreated();
                }
            }
            catch (Exception e)
            {
                LambdaLogger.Log("Failed to initialize database,");
                LambdaLogger.Log(e.Message);
            }
           
        }
    }
}
