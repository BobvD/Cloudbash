using AutoMapper;
using Cloudbash.Application.Common.Behaviours;
using Cloudbash.Application.Common.EventSourcing;
using Cloudbash.Application.Common.Interfaces;
using Cloudbash.Application.Common.Repositories;
using Cloudbash.Domain.Concerts;
using Cloudbash.Domain.Venues;
using Cloudbash.Domain.Users;
using Cloudbash.Infrastructure.Configs;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace Cloudbash.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IServerlessConfiguration config)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

            services.AddTransient<IRepository<Concert>, EventSourcedRepository<Concert>>();
            services.AddTransient<IRepository<Venue>, EventSourcedRepository<Venue>>();
            services.AddTransient<IRepository<User>, EventSourcedRepository<User>>();

            switch (config.Database)
            {
                case DatabaseType.POSTGRES:
                    services.AddTransient<IViewModelRepository<Domain.ViewModels.Concert>, ConcertEFRepository>();
                    break;
                case DatabaseType.REDIS:
                    services.AddTransient<IViewModelRepository<Domain.ViewModels.Concert>, ConcertCacheRepository>();
                    break;
                default:
                    break;
            }


            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
