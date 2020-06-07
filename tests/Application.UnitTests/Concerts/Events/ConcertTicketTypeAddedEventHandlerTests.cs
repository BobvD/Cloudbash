using Cloudbash.Application.Common.Events;
using Cloudbash.Application.Concerts.Events;
using Cloudbash.Domain.Concerts;
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
    public class ConcertTicketTypeAddedEventHandlerTests : EventHandlerTestBase
    {
        public ConcertTicketTypeAddedEventHandlerTests(TestFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public async Task When_Concert_TicketTypeAddedEvent_Handled_ConcertVm_Contains_TicketType()
        {
            var concert = await CreateAndSaveNewConcertAggregate();

            concert.ClearUncommittedEvents();

            var ticketType = new TicketType
            {
                Name = "Test",
                Quantity = 5000,
                Price = 49
            };

            concert.AddTicketType(ticketType);

            ConcertTicketTypeAddedEvent @event = (ConcertTicketTypeAddedEvent)concert.GetUncommittedEvents().Last();

            var handler = new ConcertTicketTypeAddedEventHandler(_concertRepo);

            var notification = new DomainEventNotification<ConcertTicketTypeAddedEvent>(@event);

            await handler.Handle(notification, CancellationToken.None);

            var concertVm = await _concertRepo.GetAsync(concert.Id, new[] { "TicketTypes" });

            concertVm.TicketTypes.Count.ShouldBe(1);
            concertVm.TicketTypes.First().Name.ShouldBe(ticketType.Name);

        }

    }
}