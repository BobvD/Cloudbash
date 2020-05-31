using Cloudbash.Domain.Concerts;
using Cloudbash.Domain.UnitTests.Common;
using Cloudbash.Domain.Venues;
using Cloudbash.Domain.Venues.Events;
using System;
using Xunit;

namespace Cloudbash.Domain.UnitTests
{
    public class VenueTests : AggregateRootBaseTest<Venue>
    {
   
        [Fact]
        public void When_Create_New_Venue_VenueCreatedEvent()
        {
            var venue = new Venue(
                "Ziggo Dome", 
                "Popular concert hall in Amsterdam.", 
                10000, 
                "http://website", 
                "Amsterdam");

            AssertSingleUncommittedEvent<VenueCreatedEvent>(venue, @event =>
            {
                Assert.Equal(venue.Name, @event.Name);
                Assert.Equal(venue.Description, @event.Description);
                Assert.Equal(venue.Capacity, @event.Capacity);
                Assert.Equal(venue.WebUrl, @event.WebUrl);
                Assert.Equal(venue.Address, @event.Address);
                Assert.NotEqual(default(Guid), @event.AggregateId);
            });
        }


        [Fact]
        public void Given_Venue_When_Delete_Venue_VenueDeletedEvent()
        {
            var venue = new Venue(
               "Ziggo Dome",
               "Popular concert hall in Amsterdam.",
               10000,
               "http://website",
               "Amsterdam");

            ClearUncommittedEvents(venue);

            venue.MarkAsDeleted();

            AssertSingleUncommittedEvent<VenueDeletedEvent>(venue, @event =>
            {
                Assert.Equal(venue.Id, @event.AggregateId);
                Assert.Equal(1, @event.AggregateVersion);
            });
        }
    }
}
