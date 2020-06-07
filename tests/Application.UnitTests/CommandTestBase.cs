using Cloudbash.Application.Common.EventSourcing;
using Cloudbash.Domain.Concerts;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using System.Reflection;
using Cloudbash.Application.Common.Behaviours;
using Cloudbash.Application.Common.Interfaces;
using Cloudbash.Domain.Carts;

namespace Cloudbash.Application.UnitTests
{
    public class CommandTestBase
    {
        private static IServiceScopeFactory _scopeFactory;

        public CommandTestBase()
        {
            ConcertRepo = EventSourcedRepositoryFactory<Concert>.Create();
            CartRepo = EventSourcedRepositoryFactory<Cart>.Create();

        }

        public EventSourcedRepository<Concert> ConcertRepo { get; }
        public EventSourcedRepository<Cart> CartRepo { get; }

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


        protected async Task<Cart> CreateAndSaveNewCartAggregate()
        {
            var cart = new Cart(Guid.NewGuid());

            await CartRepo.SaveAsync(cart);

            return cart;
        }

    }
}