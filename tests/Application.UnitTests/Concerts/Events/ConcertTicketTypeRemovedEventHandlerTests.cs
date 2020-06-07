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
    public class ConcertTicketTypeRemovedEventHandlerTests : EventHandlerTestBase
    {
        public ConcertTicketTypeRemovedEventHandlerTests(TestFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public async Task When_Concert_TicketTypeRemovedEvent_Handled_ConcertVm_Should_Not_Contain_TicketType()
        {
            var concert = await CreateAndSaveNewConcertAggregate();
                        
            var ticketType = new TicketType
            {
                Name = "Test",
                Quantity = 5000,
                Price = 49
            };

            concert.AddTicketType(ticketType);

            concert.ClearUncommittedEvents();

            concert.RemoveTicketType(ticketType.Id);

            ConcertTicketTypeRemovedEvent @event = (ConcertTicketTypeRemovedEvent)concert.GetUncommittedEvents().Last();

            var handler = new ConcertTicketTypeRemovedEventHandler(_concertRepo);

            var notification = new DomainEventNotification<ConcertTicketTypeRemovedEvent>(@event);

            await handler.Handle(notification, CancellationToken.None);

            var concertVm = await _concertRepo.GetAsync(concert.Id, new[] { "TicketTypes" });

            concertVm.TicketTypes.Count.ShouldBe(0);

        }

    }
}