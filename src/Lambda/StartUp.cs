using Cloudbash.Application;
using Cloudbash.Application.Concerts.Commands.CreateConcert;
using Cloudbash.Infrastructure;
using Cloudbash.Infrastructure.Persistence;
using Cloudbash.Lambda.Common;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

namespace Cloudbash.Lambda
{
    public class Startup
    {
        public static IServiceCollection BuildContainer()
        {
            var configuration = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddEnvironmentVariables()             
              .Build();

            return ConfigureServices(configuration);
        }


        private static IServiceCollection ConfigureServices(IConfigurationRoot configurationRoot)
        {
            var services = new ServiceCollection();

            services.AddApplication();
            services.AddInfrastructure(configurationRoot);
            services.AddMvc().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateConcertCommandValidator>());
         
            return services;
        }

    }
}