using Cloudbash.Application.Common.Interfaces;
using Cloudbash.Domain.ReadModels;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace Cloudbash.Application.Carts.Queries.GetCart
{
    public class GetCartQuery : IRequest<Cart>
    {
        public Guid CustomerId { get; set; }

        public class GetCartQueryHandler : IRequestHandler<GetCartQuery, Cart>
        {
            private readonly IViewModelRepository<Cart> _repository;

            public GetCartQueryHandler(IViewModelRepository<Cart> repository)
            {
                _repository = repository;
            }

            public async Task<Cart> Handle(GetCartQuery request, CancellationToken cancellationToken)
            {
                
                var children = new [] { "Items", "Items.TicketType" };
                var result = await _repository.FilterAsync(
                                    c => c.CustomerId == request.CustomerId, 
                                    children);
                
                return result.FirstOrDefault();
            }
        }
    }
}
