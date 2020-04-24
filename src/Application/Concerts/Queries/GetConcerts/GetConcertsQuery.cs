using Cloudbash.Application.Common.Interfaces;
using Cloudbash.Domain.ViewModels;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Cloudbash.Application.Concerts.Queries.GetConcerts
{
    public class GetConcertsQuery : IRequest<GetConcertsVm>
    {
        public class GetProductsQueryHandler : IRequestHandler<GetConcertsQuery, GetConcertsVm>
        {
            private readonly IViewModelRepository<Concert> _repository;

            public GetProductsQueryHandler(IViewModelRepository<Concert> repository)
            {
                _repository = repository;
            }

            public async Task<GetConcertsVm> Handle(GetConcertsQuery request, CancellationToken cancellationToken)
            {
                var concerts = _repository.Get().ToList();

                var vm = new GetConcertsVm
                {
                    Concerts = concerts,
                    Count = concerts.Count
                };

                return vm;
            }
        }
    }
}
