using Cloudbash.Application.Common.Exceptions;
using Cloudbash.Application.Common.Interfaces;
using Cloudbash.Domain.Carts;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Cloudbash.Application.Carts.Commands.AddCartItem
{
    public class AddCartItemCommand : IRequest
    {
        public Guid CartId { get; set; }
        public Guid TicketTypeId { get; set; }
        public int Quantity { get; set; }

        public class AddCartItemCommandHandler : IRequestHandler<AddCartItemCommand>
        {

            private readonly IRepository<Cart> _repository;

            public AddCartItemCommandHandler(IRepository<Cart> repository)
            {
                _repository = repository;
            }

            public async Task<Unit> Handle(AddCartItemCommand request, CancellationToken cancellationToken)
            {
                Console.WriteLine("cart id: " + request.CartId);
                var cart = await _repository.GetByIdAsync(request.CartId);

                if (cart == null)
                {
                    throw new NotFoundException(nameof(Cart), request.CartId);
                }

                cart.AddItem(request.TicketTypeId, request.Quantity);

                await _repository.SaveAsync(cart);

                return Unit.Value;
            }
        }

    }
}