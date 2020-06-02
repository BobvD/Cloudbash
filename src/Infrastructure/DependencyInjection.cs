using AutoMapper;
using Cloudbash.Application.Common.Interfaces;
using Cloudbash.Infrastructure.Configs;
using Cloudbash.Infrastructure.EventBus;
using Cloudbash.Infrastructure.Extensions;
using Cloudbash.Infrastructure.Firehose;
using Cloudbash.Infrastructure.Persistence;
using Cloudbash.Infrastructure.Persistence.EventStore;
using Cloudbash.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Cloudbash.Domain.ReadModels;

namespace Cloudbash.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services, 
            IConfigurationRoot configurationRoot, 
            IServerlessConfiguration config)
        {                        
            services.AddTransient<IEventStore, DynamoDBEventStore>();
                        
            services.AddTransient<IFileService, S3FileService>(); 
            services.AddTransient<IFirehoseClient, FirehoseClient>();
                        
            services
                .AddTransient(typeof(IAwsClientFactory<>), typeof(AwsClientFactory<>))
                .BindAndConfigure(configurationRoot.GetSection("AwsBasicConfiguration"), new AwsBasicConfiguration());

            
            switch (config.EventBus)
            {
                case EventBusType.SQS:
                    services.AddTransient<IPublisher, SQSEventBus>();
                    break;
                case EventBusType.KINESIS:
                    services.AddTransient<IPublisher, KinesisEventBus>();
                    break;
            }

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

            services.AddTransient<IViewModelRepository<Concert>, EFRepository<Concert>>();
            services.AddTransient<IViewModelRepository<TicketType>, EFRepository<TicketType>>();
            services.AddTransient<IViewModelRepository<Venue>, EFRepository<Venue>>();
            services.AddTransient<IViewModelRepository<Cart>, EFRepository<Cart>>();
        }

        private static void AddRedisRepositories(IServiceCollection services)
        {
            services.AddTransient<IViewModelRepository<Concert>, RedisRepository<Concert>>();
            services.AddTransient<IViewModelRepository<Venue>, RedisRepository<Venue>>();
        }

        private static void AddDynamoDBRepositories(IServiceCollection services)
        {
            services.AddTransient<IViewModelRepository<Concert>, DynamoDBRepository<Concert>>();
            services.AddTransient<IViewModelRepository<Venue>, DynamoDBRepository<Venue>>();
        }
    }
}
