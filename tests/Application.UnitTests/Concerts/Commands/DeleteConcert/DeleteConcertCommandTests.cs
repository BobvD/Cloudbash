using Cloudbash.Application.Common.Exceptions;
using Cloudbash.Application.Concerts.Commands.DeleteConcert;
using Shouldly;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Cloudbash.Application.UnitTests.Concerts.Commands.DeleteConcert
{
    public class DeleteConcertCommandTests : CommandTestBase
    {

        [Fact]
        public async Task Handle_Should_Mark_Concert_As_Deleted()
        {
            var concert = await CreateAndSaveNewConcertAggregate();

            var command = new DeleteConcertCommand
            {
                Id = concert.Id
            };

            var handler = new DeleteConcertCommand.DeleteConcertCommandHandler(ConcertRepo);

            var result = await handler.Handle(command, CancellationToken.None);

            var updatedConcert = await ConcertRepo.GetByIdAsync(concert.Id);
            updatedConcert.Status.ShouldBe(Domain.Concerts.ConcertStatus.DELETED);
        }

        [Fact]
        public void Handle_GivenInvalidId_ThrowsException()
        {
            var command = new DeleteConcertCommand
            {
                Id = Guid.NewGuid()
            };

            var handler = new DeleteConcertCommand.DeleteConcertCommandHandler(ConcertRepo);

            Should.ThrowAsync<NotFoundException>(() =>
                handler.Handle(command, CancellationToken.None));
        }
    }
}