using Cloudbash.Domain.Concerts;
using Cloudbash.Domain.Concerts.Events;
using Cloudbash.Domain.UnitTests.Common;
using System;
using Xunit;

namespace Cloudbash.Domain.UnitTests
{
    public class ConcertTests : AggregateRootBaseTest<Concert>
    {
        private readonly Guid venueId = Guid.NewGuid();

        [Fact]
        public void When_Create_New_Concert_ConcertCreatedEvent()
        {
            var concert = new Concert("Mumford & Sons Live", venueId, "http://image_url");

            AssertSingleUncommittedEvent<ConcertCreatedEvent>(concert, @event =>
            {
                Assert.Equal(concert.VenueId, @event.VenueId);
                Assert.Equal(concert.Name, @event.Name);
                Assert.Equal(concert.ImageUrl, @event.ImageUrl);
                Assert.NotEqual(default(Guid), @event.AggregateId);
            });
        }


        [Fact]
        public void Given_Concert_When_Publish_Concert_ConcertPublishedEvent()
        {
            var concert = new Concert("Mumford & Sons Live", venueId, "http://image_url");
            ClearUncommittedEvents(concert);

            concert.Publish();

            AssertSingleUncommittedEvent<ConcertPublishedEvent>(concert, @event =>
            {
                Assert.Equal(concert.Id, @event.AggregateId);
            });
        }

        [Fact]
        public void Given_Concert_When_Delete_Concert_ConcertDeletedEvent()
        {
            var concert = new Concert("Mumford & Sons Live", venueId, "http://image_url");
            ClearUncommittedEvents(concert);

            concert.MarkAsDeleted();

            AssertSingleUncommittedEvent<ConcertDeletedEvent>(concert, @event =>
            {
                Assert.Equal(concert.Id, @event.AggregateId);
                Assert.Equal(1, @event.AggregateVersion);
            });
        }

        [Fact]
        public void Given_Concert_When_Add_TicketType_ConcertTicketTypeAddedEvent()
        {
            var concert = new Concert("Mumford & Sons Live", venueId, "http://image_url");
            ClearUncommittedEvents(concert);

            var type = new TicketType
            {
                Name = "Regular Ticket",
                Price = 39.00M,
                Quantity = 5000
            };

            concert.AddTicketType(type);

            AssertSingleUncommittedEvent<ConcertTicketTypeAddedEvent>(concert, @event =>
            {
                Assert.Equal(concert.Id, @event.AggregateId);
                Assert.Equal(type.Name, @event.Type.Name);
                Assert.Equal(type.Price, @event.Type.Price);
                Assert.Equal(type.Quantity, @event.Type.Quantity);
                Assert.Equal(1, @event.AggregateVersion);
            });
        }

        [Fact]
        public void Given_Concert_When_Remove_TicketType_ConcertTicketTypeRemovedEvent()
        {
            var concert = new Concert("Mumford & Sons Live", venueId, "http://image_url");           

            var type = new TicketType
            {
                Name = "Regular Ticket",
                Price = 39.00M,
                Quantity = 5000
            };

            concert.AddTicketType(type);

            ClearUncommittedEvents(concert);

            concert.RemoveTicketType(type.Id);

            AssertSingleUncommittedEvent<ConcertTicketTypeRemovedEvent>(concert, @event =>
            {
                Assert.Equal(concert.Id, @event.AggregateId);
                Assert.Equal(type.Id, @event.TicketTypeId);
                Assert.Equal(2, @event.AggregateVersion);
            });
        }

        [Fact]
        public void Given_Concert_When_Schedule_ConcertScheduledEvent()
        {
            var concert = new Concert("Mumford & Sons Live", venueId, "http://image_url");
            
            ClearUncommittedEvents(concert);

            var start = new DateTime(2020,4,20,20,30,0);
            var end = new DateTime(2020, 4, 20, 22, 30, 0);

            concert.Schedule(start, end);

            AssertSingleUncommittedEvent<ConcertScheduledEvent>(concert, @event =>
            {
                Assert.Equal(concert.Id, @event.AggregateId);
                Assert.Equal(start, @event.StartDate);
                Assert.Equal(end, @event.EndDate);
                Assert.Equal(1, @event.AggregateVersion);
            });
        }

        [Fact]
        public void Given_Concert_When_Schedule_End_Before_Start_Throw_Exception()
        {
            var concert = new Concert("Mumford & Sons Live", venueId, "http://image_url");

            ClearUncommittedEvents(concert);

            var start = new DateTime(2020, 4, 20, 22, 30, 0);
            var end = new DateTime(2020, 4, 20, 20, 30, 0);
                       
            Assert.Throws<ArgumentException>(() => { concert.Schedule(start, end); });

        }

    }
}
