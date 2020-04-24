using Cloudbash.Application.Common.Interfaces;
using Cloudbash.Infrastructure.Cache;
using Cloudbash.Infrastructure.Configs;
using Cloudbash.Infrastructure.EventStream;
using Cloudbash.Infrastructure.Extensions;
using Cloudbash.Infrastructure.Persistence;
using Cloudbash.Infrastructure.Persistence.EventStore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cloudbash.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfigurationRoot configurationRoot, IServerlessConfiguration config)
        {
            
            // Inject Event Store
            services.AddTransient<IEventStore, DynamoDBEventStore>();

            // Inject AWS Configuration
            services
                .AddTransient(typeof(IAwsClientFactory<>), typeof(AwsClientFactory<>))
                .BindAndConfigure(configurationRoot.GetSection("AwsBasicConfiguration"), new AwsBasicConfiguration());
            
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

            // Inject (read) Database
            switch (config.Database)
            {
                case DatabaseType.POSTGRES:
                    services.AddDbContext<ApplicationDbContext>(opt =>
                        opt.UseNpgsql(GetPostgresConnectionString(configurationRoot), 
                        b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
                    services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());
                    break;
                case DatabaseType.REDIS:
                    services.AddTransient<ICache, RedisCache>();
                    break;
                case DatabaseType.DYNAMO:
                    services.AddTransient<IViewModelRepository<Domain.ViewModels.Concert>, DynamoDBRepository<Domain.ViewModels.Concert>>();
                    break;
                default:
                    break;
            }
               
            return services;
        }
        
        public static string GetPostgresConnectionString(IConfigurationRoot configurationRoot)
        {
            var host = configurationRoot.GetSection("POSTGRESQL_HOST").Value;
            var port = configurationRoot.GetSection("POSTGRESQL_PORT").Value;
            return $"Server={host};Port={port};Username=master;Password=password;Database=cloudbash;";
        }
    }
}
