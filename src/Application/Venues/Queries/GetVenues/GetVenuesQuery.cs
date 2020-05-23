using Cloudbash.Application.Common.Interfaces;
using Cloudbash.Domain.ReadModels;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Cloudbash.Application.Venues.Queries.GetVenues
{
    public class GetVenuesQuery : IRequest<GetVenuesVm>
    {
        public class GetVenuesQueryHandler : IRequestHandler<GetVenuesQuery, GetVenuesVm>
        {
            private readonly IViewModelRepository<Venue> _repository;

            public GetVenuesQueryHandler(IViewModelRepository<Venue> repository)
            {
                _repository = repository;
            }

            public async Task<GetVenuesVm> Handle(GetVenuesQuery request, CancellationToken cancellationToken)
            {
                var venues = await _repository.GetAllAsync();

                var vm = new GetVenuesVm
                {
                    Venues = venues,
                    Count = venues.Count
                };

                return vm;
            }
        }
    }
}
