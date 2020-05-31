using Cloudbash.Application.Common.Exceptions;
using Cloudbash.Application.Common.Interfaces;
using Cloudbash.Domain.Carts;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Cloudbash.Application.Carts.Commands.RemoveCartItem
{
    public class RemoveCartItemCommand : IRequest
    {
        public Guid CartId { get; set; }
        public Guid CartItemId { get; set; }

        public class AddCartItemCommandHandler : IRequestHandler<RemoveCartItemCommand>
        {

            private readonly IRepository<Cart> _repository;

            public AddCartItemCommandHandler(IRepository<Cart> repository)
            {
                _repository = repository;
            }

            public async Task<Unit> Handle(RemoveCartItemCommand request, CancellationToken cancellationToken)
            {
                var cart = await _repository.GetByIdAsync(request.CartId);

                if (cart == null)
                {
                    throw new NotFoundException(nameof(Cart), request.CartId);
                }

                cart.RemoveItem(request.CartItemId);

                await _repository.SaveAsync(cart);      

                return Unit.Value;
            }
        }

    }
}