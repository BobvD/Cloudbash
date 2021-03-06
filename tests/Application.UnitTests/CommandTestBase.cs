﻿using Cloudbash.Application.Common.EventSourcing;
using Cloudbash.Domain.Concerts;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using System.Reflection;
using Cloudbash.Application.Common.Behaviours;
using Cloudbash.Application.Common.Interfaces;
using Cloudbash.Domain.Carts;
using Cloudbash.Domain.Venues;
using Cloudbash.Domain.Users;

namespace Cloudbash.Application.UnitTests
{
    public class CommandTestBase
    {
        public CommandTestBase()
        {
            ConcertRepo = EventSourcedRepositoryFactory<Concert>.Create();
            CartRepo = EventSourcedRepositoryFactory<Cart>.Create();
            VenueRepo = EventSourcedRepositoryFactory<Venue>.Create();
            UserRepo = EventSourcedRepositoryFactory<User>.Create();
        }

        public EventSourcedRepository<Concert> ConcertRepo { get; }
        public EventSourcedRepository<Cart> CartRepo { get; }
        public EventSourcedRepository<Venue> VenueRepo { get; }
        public EventSourcedRepository<User> UserRepo { get; }

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

        protected async Task<User> CreateAndSaveNewUserAggregate()
        {
            var user = new User(Guid.NewGuid(), "Jane Doe", "jane@mail.com");

            await UserRepo.SaveAsync(user);

            return user;
        }

    }
}