using Cloudbash.Application.Common.Events;
using Cloudbash.Application.Concerts.Events;
using Cloudbash.Domain.Concerts.Events;
using Shouldly;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Cloudbash.Application.UnitTests.Concerts.Events
{
    [Collection("EventHandlerTests")]
    public class ConcertDeletedEventHandlerTests : EventHandlerTestBase
    {
        public ConcertDeletedEventHandlerTests(TestFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public async Task When_Concert_Deleted_Event_Handled_ConcertVm_Deleted()
        {
            var concert = await CreateAndSaveNewConcertAggregate();

            concert.ClearUncommittedEvents();

            concert.MarkAsDeleted();

            ConcertDeletedEvent @event = (ConcertDeletedEvent)concert.GetUncommittedEvents().Last();

            var handler = new ConcertDeletedEventHandler(_concertRepo);

            var notification = new DomainEventNotification<ConcertDeletedEvent>(@event);

            await handler.Handle(notification, CancellationToken.None);

            var concertVm = await _concertRepo.GetAsync(concert.Id);

            concertVm.ShouldBeNull();

        }

    }
}
