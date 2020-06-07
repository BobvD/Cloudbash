using Cloudbash.Application.Common.Events;
using Cloudbash.Application.Concerts.Events;
using Cloudbash.Domain.Concerts.Events;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Cloudbash.Application.UnitTests.Concerts.Events
{
    [Collection("EventHandlerTests")]
    public class ConcertPublishedEventHandlerTests : EventHandlerTestBase
    {
        public ConcertPublishedEventHandlerTests(TestFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public async Task When_Concert_Published_Event_Handled_ConcertVm_Status_Changed()
        {
            var concert = await CreateAndSaveNewConcertAggregate();

            concert.ClearUncommittedEvents();

            concert.Publish();

            ConcertPublishedEvent @event = (ConcertPublishedEvent)concert.GetUncommittedEvents().Last();

            var handler = new ConcertPublishedEventHandler(_concertRepo);

            var notification = new DomainEventNotification<ConcertPublishedEvent>(@event);

            await handler.Handle(notification, CancellationToken.None);

            var concertVm = await _concertRepo.GetAsync(concert.Id);

            concertVm.Status.ShouldBe(Domain.Concerts.ConcertStatus.PUBLISHED);
            
        }

    }
}
