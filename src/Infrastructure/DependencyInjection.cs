using Cloudbash.Application.Common.Interfaces;
using Cloudbash.Infrastructure.Configs;
using Cloudbash.Infrastructure.EventStream;
using Cloudbash.Infrastructure.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cloudbash.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfigurationRoot configurationRoot)
        {

            services
                .AddTransient(typeof(IAwsClientFactory<>), typeof(AwsClientFactory<>))
                .BindAndConfigure(configurationRoot.GetSection("AwsBasicConfiguration"), new AwsBasicConfiguration());
            services.AddTransient<IPublisher, KinesisEventStream>();
            return services;
        }
    }
}
