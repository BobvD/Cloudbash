using Cloudbash.Application.Carts.Commands.CreateCart;
using Shouldly;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Cloudbash.Application.UnitTests.Carts.Commands.CreateCart
{
    public class CreateCartCommandTests : CommandTestBase
    {
        [Fact]
        public async Task Handle_ShouldPersist_Cart()
        {
            var command = new CreateCartCommand
            {
                CustomerId = Guid.NewGuid()
            };

            var handler = new CreateCartCommand.CreateCartCommandHandler(CartRepo);

            var result = await handler.Handle(command, CancellationToken.None);

            var updatedCart = await CartRepo.GetByIdAsync(result);
            updatedCart.ShouldNotBeNull();
            updatedCart.CustomerId.ShouldBe(command.CustomerId);
        }
    }
}