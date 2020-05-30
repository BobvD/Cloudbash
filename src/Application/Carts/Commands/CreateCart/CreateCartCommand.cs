using Cloudbash.Application.Common.Interfaces;
using Cloudbash.Domain.Carts;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Cloudbash.Application.Carts.Commands.CreateCart
{
    public class CreateCartCommand : IRequest<Guid>
    {
        public Guid CustomerId { get; set; }

        public class CreateCartCommandHandler : IRequestHandler<CreateCartCommand, Guid>
        {

            private readonly IRepository<Cart> _repository;

            public CreateCartCommandHandler(IRepository<Cart> repository)
            {
                _repository = repository;
            }

            public async Task<Guid> Handle(CreateCartCommand request, CancellationToken cancellationToken)
            {               
                var cart = new Cart(request.CustomerId);

                await _repository.SaveAsync(cart);

                return cart.Id;
            }
        }

    }
}
