using Cloudbash.Application.Common.Exceptions;
using Cloudbash.Application.Common.Interfaces;
using Cloudbash.Domain.Carts;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Cloudbash.Application.Carts.Commands.CheckOutCart
{
    public class CheckOutCartCommand : IRequest
    {
        public Guid CartId { get; set; }

        public class CheckOutCartCommandHandler : IRequestHandler<CheckOutCartCommand>
        {

            private readonly IRepository<Cart> _repository;

            public CheckOutCartCommandHandler(IRepository<Cart> repository)
            {
                _repository = repository;
            }

            public async Task<Unit> Handle(CheckOutCartCommand request, CancellationToken cancellationToken)
            {
                var cart = await _repository.GetByIdAsync(request.CartId);

                if (cart == null)
                {
                    throw new NotFoundException(nameof(Cart), request.CartId);
                }

                cart.CheckOut();

                await _repository.SaveAsync(cart);

                return Unit.Value;
            }
        }

    }
}