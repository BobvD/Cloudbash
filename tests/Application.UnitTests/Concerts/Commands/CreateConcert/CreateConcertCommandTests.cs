using Cloudbash.Application.Common.Exceptions;
using Cloudbash.Application.Concerts.Commands.CreateConcert;
using Shouldly;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Cloudbash.Application.UnitTests.Concerts.Commands.CreateConcert
{
    public class CreateConcertCommandTests : CommandTestBase
    {
        private readonly Guid venueId = Guid.NewGuid();

        [Fact]
        public async Task Handle_ShouldPersist_Concert()
        {
            var command = new CreateConcertCommand
            {
               Name = "Mumford & Sons",
               VenueId = venueId,
               ImageUrl = "IMAGE_URL"
            };

            var handler = new CreateConcertCommand.CreateConcertCommandHandler(ConcertRepo);

            var result = await handler.Handle(command, CancellationToken.None);

            var concert = await ConcertRepo.GetByIdAsync(result);
            concert.Name.ShouldBe(command.Name);
            concert.VenueId.ShouldBe(command.VenueId);
            concert.ImageUrl.ShouldBe(command.ImageUrl);
        }
    }
}
