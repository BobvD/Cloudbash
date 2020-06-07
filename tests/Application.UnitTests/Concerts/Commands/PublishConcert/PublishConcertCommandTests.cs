using Cloudbash.Application.Common.Exceptions;
using Cloudbash.Application.Concerts.Commands.DeleteConcert;
using Cloudbash.Application.Concerts.Commands.PublishConcert;
using Shouldly;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Cloudbash.Application.UnitTests.Concerts.Commands.PublishConcert
{
    public class PublishConcertCommandTests : CommandTestBase
    {

        [Fact]
        public async Task Handle_Should_Mark_Concert_As_Published()
        {
            var concert = await CreateAndSaveNewConcertAggregate();

            var command = new PublishConcertCommand
            {
                Id = concert.Id
            };

            var handler = new PublishConcertCommand.PublishConcertCommandHandler(ConcertRepo);

            var result = await handler.Handle(command, CancellationToken.None);

            var updatedConcert = await ConcertRepo.GetByIdAsync(concert.Id);
            updatedConcert.Status.ShouldBe(Domain.Concerts.ConcertStatus.PUBLISHED);
        }

        [Fact]
        public async Task Handle_GivenInvalidId_ThrowsExceptionAsync()
        {
            var command = new PublishConcertCommand
            {
                Id = Guid.NewGuid()
            };

            var handler = new PublishConcertCommand.PublishConcertCommandHandler(ConcertRepo);

            await Should.ThrowAsync<NotFoundException>(() => handler.Handle(command, CancellationToken.None));
        }
    }
}