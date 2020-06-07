using Cloudbash.Application.Carts.Commands.AddCartItem;
using Cloudbash.Application.Common.Exceptions;
using Shouldly;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Cloudbash.Application.UnitTests.Carts.Commands.AddCartItem
{
    public class AddCartItemCommandTests : CommandTestBase
    {      
        [Fact]
        public async Task Handle_ShouldPersist_CartItem()
        {
            var cart = await CreateAndSaveNewCartAggregate();

            var command = new AddCartItemCommand
            {
                CartId = cart.Id,
                TicketTypeId = Guid.NewGuid(),
                Quantity = 4
            };

            var handler = new AddCartItemCommand.AddCartItemCommandHandler(CartRepo);

            var result = await handler.Handle(command, CancellationToken.None);

            var updatedCart = await CartRepo.GetByIdAsync(cart.Id);
            updatedCart.Items.Count.ShouldBe(1);
        }

        [Fact]
        public async Task Handle_GivenInvalidCartId_ThrowsExceptionAsync()
        {
            var command = new AddCartItemCommand
            {
                CartId = Guid.NewGuid()
            };

            var handler = new AddCartItemCommand.AddCartItemCommandHandler(CartRepo);

            await Should.ThrowAsync<NotFoundException>(() => handler.Handle(command, CancellationToken.None));
        }

    }
}