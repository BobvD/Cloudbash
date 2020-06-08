using Cloudbash.Application.Users.Commands.CreateUser;
using Shouldly;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Cloudbash.Application.UnitTests.Users.Commands.CreateUser
{
    public class CreateUserCommandTests : CommandTestBase
    {
        [Fact]
        public async Task Handle_ShouldPersist_Cart()
        {
            var command = new CreateUserCommand
            {
                Id = Guid.NewGuid(),
                FullName = "John Doe",
                Email = "john@mail.com"
            };

            var handler = new CreateUserCommand.CreateUserCommandHandler(UserRepo);

            var result = await handler.Handle(command, CancellationToken.None);

            var savedUser = await UserRepo.GetByIdAsync(result);
            savedUser.ShouldNotBeNull();
            savedUser.Id.ShouldBe(command.Id);
            savedUser.FullName.ShouldBe(command.FullName);
            savedUser.Email.ShouldBe(command.Email);
        }
    }
}