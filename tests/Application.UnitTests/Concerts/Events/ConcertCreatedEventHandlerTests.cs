using Cloudbash.Application.Common.Events;
using Cloudbash.Application.Concerts.Events;
using Cloudbash.Domain.Concerts.Events;
using Shouldly;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Cloudbash.Application.UnitTests.Concerts.Events
{
    [Collection("EventHandlerTests")]
    public class ConcertCreatedEventHandlerTests : EventHandlerTestBase
    {
        public ConcertCreatedEventHandlerTests(TestFixture fixture) : base (fixture)
        {
        }

        [Fact]
        public async Task When_Concert_Created_Event_Handled_ConcertVm_Saved()
        {
            var concert = new Domain.Concerts.Concert(
                    "Lady Gaga", 
                    new Guid("5b5eb886-06c6-4aed-bb24-35a1373edf97"), 
                    "IMAGE_URL"
                    );

            ConcertCreatedEvent @event = (ConcertCreatedEvent)concert.GetUncommittedEvents().Last();

            var handler = new ConcertCreatedEventHandler(_concertRepo, _venueRepo);
            
            var notification = new DomainEventNotification<ConcertCreatedEvent>(@event);

            await handler.Handle(notification, CancellationToken.None);

            var concerts = await _concertRepo.GetAllAsync();
            
            concerts.Count.ShouldBe(2);

        }  

    }
}
