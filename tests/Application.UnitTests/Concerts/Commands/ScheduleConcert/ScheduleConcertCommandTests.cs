using Cloudbash.Application.Common.Exceptions;
using Cloudbash.Application.Concerts.Commands.ScheduleConcert;
using Shouldly;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Cloudbash.Application.UnitTests.Concerts.Commands.ScheduleConcert
{
    public class ScheduleConcertCommandTests : CommandTestBase
    {

        [Fact]
        public async Task Handle_Should_Update_Concert_Dates()
        {
            var concert = await CreateAndSaveNewConcertAggregate();

            var command = new ScheduleConcertCommand
            {
                ConcertId = concert.Id,
                StartDate = new DateTime(2020, 10, 3),
                EndDate = new DateTime(2020, 10, 4)               
            };

            var handler = new ScheduleConcertCommand.ScheduleConcertCommandHandler(ConcertRepo);

            var result = await handler.Handle(command, CancellationToken.None);

            var updatedConcert = await ConcertRepo.GetByIdAsync(concert.Id);
            updatedConcert.StartDate.ShouldBe(command.StartDate);
            updatedConcert.EndDate.ShouldBe(command.EndDate);
        }

        [Fact]
        public async Task Handle_GivenInvalidConcertId_ThrowsExceptionAsync()
        {
            var command = new ScheduleConcertCommand
            {
                ConcertId = Guid.NewGuid(),
                StartDate = new DateTime(2020, 10, 3),
                EndDate = new DateTime(2020, 10, 4)
            };

            var handler = new ScheduleConcertCommand.ScheduleConcertCommandHandler(ConcertRepo);
            
           await Should.ThrowAsync<NotFoundException>(() =>handler.Handle(command, CancellationToken.None));
        }
    }
}