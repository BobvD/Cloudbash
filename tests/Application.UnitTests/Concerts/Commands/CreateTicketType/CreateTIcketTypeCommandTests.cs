using Cloudbash.Application.Common.Exceptions;
using Cloudbash.Application.Concerts.Commands.CreateTicketType;
using Shouldly;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Cloudbash.Application.UnitTests.Concerts.Commands.CreateTicketType
{
    public class CreateTicketTypeCommandTests : CommandTestBase
    {

        [Fact]
        public async Task Handle_ShouldPersist_TicketType()
        {
            var concert = await CreateAndSaveNewConcertAggregate();

            var command = new CreateTicketTypeCommand
            {
                ConcertId = concert.Id,
                Name = "Regular Ticket",
                Price = 49,
                Quantity = 5000
            };

            var handler = new CreateTicketTypeCommand.CreateTicketTypeCommandHandler(ConcertRepo);

            var result = await handler.Handle(command, CancellationToken.None);

            var updatedConcert = await ConcertRepo.GetByIdAsync(concert.Id);           
            updatedConcert.TicketTypes.Count.ShouldBe(1);
            updatedConcert.TicketTypes.First().Name.ShouldBe(command.Name);           
        }

        [Fact]
        public void Handle_GivenInvalidConcertId_ThrowsException()
        {
            var command = new CreateTicketTypeCommand
            {
                ConcertId = Guid.NewGuid(),
                Name = "Regular Ticket",
                Price = 49,
                Quantity = 5000
            };

            var handler = new CreateTicketTypeCommand.CreateTicketTypeCommandHandler(ConcertRepo);

            Should.ThrowAsync<NotFoundException>(() =>
                handler.Handle(command, CancellationToken.None));
        }


        [Fact]
        public void ShouldRequireMinimumFields()
        {
            var command = new CreateTicketTypeCommand();

            Should.ThrowAsync<ValidationException>(() => SendAsync(command));
        }
    }
}