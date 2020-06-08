using Cloudbash.Application.Venues.Commands.CreateVenue;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Cloudbash.Application.UnitTests.Venues.Commands.CreateVenue
{
    public class CreateVenueCommandTests : CommandTestBase
    {
        [Fact]
        public async Task Handle_ShouldPersist_Cart()
        {
            var command = new CreateVenueCommand
            {
                Name = "De Helling",
                Description = "Poppodium",
                Capacity = 2500,
                WebUrl = "WEB_URL",
                Address = "Utrecht"
            };

            var handler = new CreateVenueCommand.CreateVenueCommandHandler(VenueRepo);

            var result = await handler.Handle(command, CancellationToken.None);

            var savedVenue = await VenueRepo.GetByIdAsync(result);
            savedVenue.ShouldNotBeNull();
            savedVenue.Name.ShouldBe(command.Name);
            savedVenue.Description.ShouldBe(command.Description);
            savedVenue.Capacity.ShouldBe(command.Capacity);
            savedVenue.WebUrl.ShouldBe(command.WebUrl);
            savedVenue.Address.ShouldBe(command.Address);
        }
    }
}