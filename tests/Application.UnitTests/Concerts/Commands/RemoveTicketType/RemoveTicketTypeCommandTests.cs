using Cloudbash.Application.Common.Exceptions;
using Cloudbash.Application.Concerts.Commands.RemoveTicketType;
using Cloudbash.Domain.Concerts;
using Shouldly;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Cloudbash.Application.UnitTests.Concerts.Commands.RemoveTicketType
{
    public class RemoveTicketTypeCommandTests : CommandTestBase
    {

        [Fact]
        public async Task Handle_ShouldRemove_TicketType()
        {
            var concert = await CreateAndSaveNewConcertAggregate();

            var ticketType = new TicketType
            {
                Name = "Regular Ticket",
                Price = 49,
                Quantity = 5000
            };

            concert.AddTicketType(ticketType);

            await ConcertRepo.SaveAsync(concert);

            var command = new RemoveTicketTypeCommand
            {
                ConcertId = concert.Id,
                TicketTypeId = ticketType.Id
            };

            var handler = new RemoveTicketTypeCommand.RemoveTicketTypeCommandHandler(ConcertRepo);

            var result = await handler.Handle(command, CancellationToken.None);

            var updatedConcert = await ConcertRepo.GetByIdAsync(concert.Id);
            updatedConcert.TicketTypes.Count.ShouldBe(0);
        }

        [Fact]
        public void Handle_GivenInvalidConcertId_ThrowsException()
        {
            var command = new RemoveTicketTypeCommand
            {
                ConcertId = Guid.NewGuid(),
                TicketTypeId = Guid.NewGuid()
            };

            var handler = new RemoveTicketTypeCommand.RemoveTicketTypeCommandHandler(ConcertRepo);

            Should.ThrowAsync<NotFoundException>(() =>
                handler.Handle(command, CancellationToken.None));
        }
    }
}