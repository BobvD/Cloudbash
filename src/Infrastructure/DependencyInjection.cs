using AutoMapper;
using Cloudbash.Application.Common.Interfaces;
using Cloudbash.Infrastructure.Cache;
using Cloudbash.Infrastructure.Configs;
using Cloudbash.Infrastructure.EventStream;
using Cloudbash.Infrastructure.Extensions;
using Cloudbash.Infrastructure.Firehose;
using Cloudbash.Infrastructure.Persistence;
using Cloudbash.Infrastructure.Persistence.EventStore;
using Cloudbash.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Cloudbash.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfigurationRoot configurationRoot, IServerlessConfiguration config)
        {

            // Inject Event Store
            services.AddTransient<IEventStore, DynamoDBEventStore>();

            // Inject Services
            services.AddTransient<IFileService, S3FileService>(); 
            services.AddTransient<IFirehoseClient, FirehoseClient>();

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
                    AddEFRepositories(services, configurationRoot);
                    break;
                case DatabaseType.REDIS:
                    AddRedisRepositories(services);
                    break;
                case DatabaseType.DYNAMO:
                    AddDynamoDBRepositories(services);
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

        private static void AddEFRepositories(IServiceCollection services, IConfigurationRoot configurationRoot)
        {
            services.AddDbContext<ApplicationDbContext>(opt =>
                        opt.UseNpgsql(GetPostgresConnectionString(configurationRoot),
                        b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            services.AddTransient<IViewModelRepository<Domain.ViewModels.Concert>, EFRepository<Domain.ViewModels.Concert>>();
            services.AddTransient<IViewModelRepository<Domain.ViewModels.TicketType>, EFRepository<Domain.ViewModels.TicketType>>();
            services.AddTransient<IViewModelRepository<Domain.ViewModels.Venue>, EFRepository<Domain.ViewModels.Venue>>();
        }

        private static void AddRedisRepositories(IServiceCollection services)
        {
            services.AddTransient<IViewModelRepository<Domain.ViewModels.Concert>, RedisRepository<Domain.ViewModels.Concert>>();
            services.AddTransient<IViewModelRepository<Domain.ViewModels.Venue>, RedisRepository<Domain.ViewModels.Venue>>();
        }

        private static void AddDynamoDBRepositories(IServiceCollection services)
        {
            services.AddTransient<IViewModelRepository<Domain.ViewModels.Concert>, DynamoDBRepository<Domain.ViewModels.Concert>>();
            services.AddTransient<IViewModelRepository<Domain.ViewModels.Venue>, DynamoDBRepository<Domain.ViewModels.Venue>>();
        }
    }
}
