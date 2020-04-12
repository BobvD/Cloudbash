using Cloudbash.Application.Common.Behaviours;
using Cloudbash.Application.Common.EventSourcing;
using Cloudbash.Application.Common.Interfaces;
using Cloudbash.Application.Common.Repositories;
using Cloudbash.Domain.Entities;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Cloudbash.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddTransient<IRepository<Concert>, EventSourcedRepository<Concert>>();
            services.AddTransient<IRepository<User>, EventSourcedRepository<User>>();

            services.AddTransient<IViewModelRepository<Domain.ViewModels.Concert>, ConcertRepository>();

            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));


            return services;
        }
    }
}
