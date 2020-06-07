using Cloudbash.Application.Common.EventSourcing;
using Cloudbash.Domain.Concerts;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using MediatR;

namespace Cloudbash.Application.UnitTests
{
    public class CommandTestBase
    {
        private static IServiceScopeFactory _scopeFactory;

        public CommandTestBase()
        {
            ConcertRepo = EventSourcedRepositoryFactory<Concert>.Create();

            var services = new ServiceCollection();

            services.AddApplication(null);

            _scopeFactory = services.BuildServiceProvider().GetService<IServiceScopeFactory>();
        }

        public EventSourcedRepository<Concert> ConcertRepo { get; }

        public static async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
        {
            using var scope = _scopeFactory.CreateScope();

            var mediator = scope.ServiceProvider.GetService<IMediator>();

            return await mediator.Send(request);
        }

        protected async Task<Concert> CreateAndSaveNewConcertAggregate()
        {
            var concert = new Concert ( 
                    null,
                    new Guid("2bfb53d2-549d-438b-a8be-e87a7457abd7"),
                    null
                );

            await ConcertRepo.SaveAsync(concert);

            return concert;
        }

    }
}