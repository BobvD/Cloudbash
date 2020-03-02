using Cloudbash.Application;
using Cloudbash.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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

            return services;
        }
    }
}