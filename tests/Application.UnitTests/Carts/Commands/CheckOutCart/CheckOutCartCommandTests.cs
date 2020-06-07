using Cloudbash.Application.Carts.Commands.CheckOutCart;
using Cloudbash.Application.Common.Exceptions;
using Cloudbash.Domain.Carts;
using Shouldly;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Cloudbash.Application.UnitTests.Carts.Commands.CheckOutCart
{
    public class CheckOutCartCommandTests : CommandTestBase
    {
        [Fact]
        public async Task Handle_Should_Check_Out_Cart()
        {
            var cart = await CreateAndSaveNewCartAggregate();

            var item = new CartItem
            {
                TicketTypeId = Guid.NewGuid(),
                Quantity = 2
            };

            cart.AddItem(item);

            await CartRepo.SaveAsync(cart);
            
            var command = new CheckOutCartCommand
            {
                CartId = cart.Id
            };

            var handler = new CheckOutCartCommand.CheckOutCartCommandHandler(CartRepo);

            var result = await handler.Handle(command, CancellationToken.None);

            var updatedCart = await CartRepo.GetByIdAsync(cart.Id);
            updatedCart.IsCheckedOut.ShouldBeTrue();
        }

        [Fact]
        public async Task Handle_GivenInvalidCartId_ThrowsExceptionAsync()
        {
            var command = new CheckOutCartCommand
            {
                CartId = Guid.NewGuid()
            };


            var handler = new CheckOutCartCommand.CheckOutCartCommandHandler(CartRepo);

            await Should.ThrowAsync<NotFoundException>(() => handler.Handle(command, CancellationToken.None));
        }
    }
}