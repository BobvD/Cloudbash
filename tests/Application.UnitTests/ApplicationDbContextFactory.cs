using Cloudbash.Domain.ReadModels;
using Cloudbash.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Cloudbash.Application.UnitTests
{
    public static class ApplicationDbContextFactory
    {
        public static ApplicationDbContext Create()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;


            var context = new ApplicationDbContext(options);

            context.Database.EnsureCreated();

            SeedSampleData(context);

            return context;
        }

        public static void SeedSampleData(ApplicationDbContext context)
        {
            Venue venue1 = new Venue
            {
                Id = "5b5eb886-06c6-4aed-bb24-35a1373edf97",
                Name = "Ziggo Dome",
                Description = "Music Hall",
                Capacity = 20000,
                WebUrl = "WEB_URL",
                Address = "Amsterdam"
            };

            context.Add(venue1);

            Concert concert1 = new Concert
            {
                Id = "2bfb53d2-549d-438b-a8be-e87a7457abd7",
                Name = "Mumford & Sons",
                VenueId = "5b5eb886-06c6-4aed-bb24-35a1373edf97",
                ImageUrl = "IMAGE_URL",
                StartDate = new DateTime(2020, 12, 10, 8, 30, 00),
                EndDate = new DateTime(2020, 12, 10, 10, 30, 00),
                Status = Domain.Concerts.ConcertStatus.PUBLISHED,
                Created = new DateTime(2020, 2, 20),
            };

            context.Add(concert1);

            TicketType ticketType1 = new TicketType
            {
                Id = "8e93b939-c7f8-4de4-9b67-9301bbe7b004",
                Name = "Regular Ticket",
                Price = 49,
                Quantity = 19000,
                Concert = concert1
            };

            context.Add(ticketType1);

            TicketType ticketType2 = new TicketType
            {
                Id = "4f356bc0-ebe0-409a-aea7-d7fdd7b76154",
                Name = "VIP Ticket",
                Price = 490,
                Quantity = 1000,
                Concert = concert1
            };

            context.Add(ticketType2);


            Cart cart = new Cart
            {
                Id = "e43b8063-23f2-4835-a35a-503ae5e09673",
                CustomerId = new Guid("6fe6c9a1-2ee9-4413-9123-d840ca7ead49"),
                Items = new List<CartItem>
                {
                    new CartItem { Id = "313e977f-6c35-4dd3-a162-cb75c343e698", Quantity = 2, TicketType = ticketType1},
                    new CartItem { Id = "d00247ee-cf2e-4319-ae77-e24b826d6946", Quantity = 4, TicketType = ticketType2}
                }
            };

            context.Add(cart);

            context.SaveChanges();
        }
        
        public static void Destroy(ApplicationDbContext context)
        {
            context.Database.EnsureDeleted();

            context.Dispose();
        }
    }
}