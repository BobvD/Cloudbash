using Cloudbash.Application.Carts.Commands.CreateCart;
using Cloudbash.Application.Carts.Commands.RemoveCartItem;
using Cloudbash.Application.Common.Exceptions;
using Cloudbash.Domain.Carts;
using Shouldly;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Cloudbash.Application.UnitTests.Carts.Commands.RemoveCartItem
{
    public class RemoveCartItemCommandTests : CommandTestBase
    {
        [Fact]
        public async Task Handle_ShouldPersist_Cart()
        {
            var cart = await CreateAndSaveNewCartAggregate();

            var item = new CartItem
            {
                TicketTypeId = Guid.NewGuid(),
                Quantity = 2
            };

            cart.AddItem(item);

            await CartRepo.SaveAsync(cart);

            var command = new RemoveCartItemCommand
            {
                CartId = cart.Id,
                CartItemId = item.Id
            };

            var handler = new RemoveCartItemCommand.RemoveCartItemCommandHandler(CartRepo);

            var result = await handler.Handle(command, CancellationToken.None);

            var updatedCart = await CartRepo.GetByIdAsync(cart.Id);
            updatedCart.ShouldNotBeNull();
            updatedCart.Items.Count.ShouldBe(0);
        }

        [Fact]
        public async Task Handle_GivenInvalidCartId_ThrowsExceptionAsync()
        {
            var command = new RemoveCartItemCommand
            {
                CartId = Guid.NewGuid()
            };
            
            var handler = new RemoveCartItemCommand.RemoveCartItemCommandHandler(CartRepo);

            await Should.ThrowAsync<NotFoundException>(() => handler.Handle(command, CancellationToken.None));
        }
    }
}