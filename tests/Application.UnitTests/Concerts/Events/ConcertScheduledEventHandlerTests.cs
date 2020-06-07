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
    public class ConcertScheduledEventHandlerTests : EventHandlerTestBase
    {
        public ConcertScheduledEventHandlerTests(TestFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public async Task When_Concert_Scheduled_Event_Handled_ConcertVm_Dates_Changed()
        {
            var concert = await CreateAndSaveNewConcertAggregate();

            concert.ClearUncommittedEvents();

            var start = new DateTime(2020, 2, 1, 10, 00, 00);
            var end = new DateTime(2020, 2, 1, 12, 00, 00);

            concert.Schedule(start, end);

            ConcertScheduledEvent @event = (ConcertScheduledEvent)concert.GetUncommittedEvents().Last();

            var handler = new ConcertScheduledEventHandler(_concertRepo);

            var notification = new DomainEventNotification<ConcertScheduledEvent>(@event);

            await handler.Handle(notification, CancellationToken.None);

            var concertVm = await _concertRepo.GetAsync(concert.Id);

            concertVm.StartDate.ShouldBe(start);
            concertVm.EndDate.ShouldBe(end);

        }

    }
}
