using AutoMapper;
using Cloudbash.Application.Common.Behaviours;
using Cloudbash.Application.Common.EventSourcing;
using Cloudbash.Application.Common.Interfaces;
using Cloudbash.Domain.Concerts;
using Cloudbash.Domain.Venues;
using Cloudbash.Domain.Users;
using Cloudbash.Infrastructure.Configs;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Cloudbash.Domain.Carts;

namespace Cloudbash.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IServerlessConfiguration config)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

            services.AddTransient<IRepository<Concert>, EventSourcedRepository<Concert>>();
            services.AddTransient<IRepository<Venue>, EventSourcedRepository<Venue>>();
            services.AddTransient<IRepository<User>, EventSourcedRepository<User>>();
            services.AddTransient<IRepository<Cart>, EventSourcedRepository<Cart>>();

            return services;
        }
    }
}
