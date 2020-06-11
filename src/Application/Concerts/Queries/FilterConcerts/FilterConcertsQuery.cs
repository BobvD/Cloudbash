using Cloudbash.Application.Common.Interfaces;
using Cloudbash.Domain.ReadModels;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Cloudbash.Application.Concerts.Queries.FilterConcerts
{
    public class FilterConcertsQuery : IRequest<FilterConcertsVm>
    {
        public string SearchTerm { get; set; }

        public class FilterConcertsQueryHandler : IRequestHandler<FilterConcertsQuery, FilterConcertsVm>
        {
            private readonly IViewModelRepository<Concert> _repository;

            public FilterConcertsQueryHandler(IViewModelRepository<Concert> repository)
            {
                _repository = repository;
            }

            public async Task<FilterConcertsVm> Handle(FilterConcertsQuery request, CancellationToken cancellationToken)
            {
                var children = new[] { "Venue" };

                var concerts = await _repository.FilterAsync(
                    c => c.Name.ToLower().Contains(request.SearchTerm.ToLower()) 
                    || c.Venue.Name.ToLower().Contains(request.SearchTerm.ToLower()),
                    children);

                var vm = new FilterConcertsVm
                {
                    Concerts = concerts,
                    Count = concerts.Count
                };

                return vm;
            }
        }
    }
}

