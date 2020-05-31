using Cloudbash.Application.Common.Exceptions;
using Cloudbash.Application.Common.Interfaces;
using Cloudbash.Domain.Carts;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Cloudbash.Application.Carts.Commands.AddCartItem
{
    public class AddCartItemCommand : IRequest<Guid>
    {
        public Guid CartId { get; set; }
        public Guid TicketTypeId { get; set; }
        public int Quantity { get; set; }

        public class AddCartItemCommandHandler : IRequestHandler<AddCartItemCommand, Guid>
        {

            private readonly IRepository<Cart> _repository;

            public AddCartItemCommandHandler(IRepository<Cart> repository)
            {
                _repository = repository;
            }

            public async Task<Guid> Handle(AddCartItemCommand request, CancellationToken cancellationToken)
            {
                var cart = await _repository.GetByIdAsync(request.CartId);

                if (cart == null)
                {
                    throw new NotFoundException(nameof(Cart), request.CartId);
                }

                var cartItem = new CartItem
                {
                    TicketTypeId = request.TicketTypeId,
                    Quantity = request.Quantity
                };
                
                cart.AddItem(cartItem);

                await _repository.SaveAsync(cart);

                return cartItem.Id;
            }
        }

    }
}