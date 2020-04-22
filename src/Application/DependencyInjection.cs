using Cloudbash.Application.Common.Behaviours;
using Cloudbash.Application.Common.EventSourcing;
using Cloudbash.Application.Common.Interfaces;
using Cloudbash.Application.Common.Repositories;
using Cloudbash.Domain.Entities;
using Cloudbash.Infrastructure.Configs;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Cloudbash.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IServerlessConfiguration config)
        {
            
            services.AddTransient<IRepository<Concert>, EventSourcedRepository<Concert>>();
            services.AddTransient<IRepository<User>, EventSourcedRepository<User>>();

            switch (config.Database)
            {
                case DatabaseType.POSTGRES:
                    services.AddTransient<IViewModelRepository<Domain.ViewModels.Concert>, ConcertEFRepository>();
                    break;
                case DatabaseType.REDIS:
                    break;
                default:
                    break;
            }
            

            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));


            return services;
        }
    }
}
