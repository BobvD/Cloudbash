using Cloudbash.Application.Common.Interfaces;
using Cloudbash.Infrastructure.Configs;
using Cloudbash.Infrastructure.EventStream;
using Cloudbash.Infrastructure.Extensions;
using Cloudbash.Infrastructure.Persistence;
using Cloudbash.Infrastructure.Persistence.EventStore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Cloudbash.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfigurationRoot configurationRoot)
        {
            // Get the configuration
            var config = new ServerlessConfiguration();
            configurationRoot.Bind("ServerlessConfiguration", config);
            services.AddSingleton(config);

            // Inject Event stream
            switch (config.EventBus)
            {
                case EventBusType.SQS:
                    services.AddTransient<IPublisher, SQSEventStream>();
                    break;
                case EventBusType.KINESIS:
                    services.AddTransient<IPublisher, KinesisEventStream>();
                    break;
                default:
                    break;
            }            

            services.AddTransient<IEventStore, DynamoDBEventStore>();


            services
                .AddTransient(typeof(IAwsClientFactory<>), typeof(AwsClientFactory<>))
                .BindAndConfigure(configurationRoot.GetSection("AwsBasicConfiguration"), new AwsBasicConfiguration());

            services.AddDbContext<ApplicationDbContext>(opt =>
                opt.UseNpgsql(GetConnectionString(configurationRoot), b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
            
            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());


            return services;
        }
        
        public static string GetConnectionString(IConfigurationRoot configurationRoot)
        {
            var host = configurationRoot.GetSection("POSTGRESQL_HOST").Value;
            Console.WriteLine(host);
            var port = configurationRoot.GetSection("POSTGRESQL_PORT").Value;
            Console.WriteLine(port);

            return $"Server={host};Port={port};Username=master;Password=password;Database=cloudbash;";
        }
    }
}
