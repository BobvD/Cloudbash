using Cloudbash.Application.Users.Commands.AddUserActivityLog;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Cloudbash.Application.UnitTests.Users.Commands.AddUserActivityLog
{
    public class AddUserActivityLogCommandTests : CommandTestBase
    {
        [Fact]
        public async Task Handle_ShouldPersist_Cart()
        {
            var user = await CreateAndSaveNewUserAggregate();

            var command = new AddUserActivityLogCommand
            {
                UserId = user.Id,
                ActivityType = Domain.Users.UserActivityType.AUTHENTICATION
            };

            var handler = new AddUserActivityLogCommand.AddUserActivityLogCommandHandler(UserRepo);

            var result = await handler.Handle(command, CancellationToken.None);

            var updatedUser = await UserRepo.GetByIdAsync(user.Id);
            updatedUser.Activities.Last().Activity.ShouldBe(command.ActivityType);
            updatedUser.Activities.Last().UserID.ShouldBe(user.Id);
        }
    }
}