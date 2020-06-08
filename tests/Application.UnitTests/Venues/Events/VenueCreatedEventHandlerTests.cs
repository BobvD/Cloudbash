using Cloudbash.Application.Common.Events;
using Cloudbash.Application.Concerts.Events;
using Cloudbash.Application.Venues.Events;
using Cloudbash.Domain.Concerts.Events;
using Cloudbash.Domain.Venues.Events;
using Shouldly;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Cloudbash.Application.UnitTests.Venues.Events
{
    [Collection("EventHandlerTests")]
    public class VenueCreatedEventHandlerTests : EventHandlerTestBase
    {
        public VenueCreatedEventHandlerTests(TestFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public async Task When_Venue_Created_Event_Handled_VenueVm_Saved()
        {
            var venue = new Domain.Venues.Venue("Amsterdam Arena", "Voetbal stadion", 55000, "WEB_URL", "Amsterdam");

            VenueCreatedEvent @event = (VenueCreatedEvent)venue.GetUncommittedEvents().Last();

            var handler = new VenueCreatedEventHandler(_venueRepo);

            var notification = new DomainEventNotification<VenueCreatedEvent>(@event);

            await handler.Handle(notification, CancellationToken.None);

            var concerts = await _venueRepo.GetAllAsync();

            concerts.Count.ShouldBe(2);

        }

    }
}
