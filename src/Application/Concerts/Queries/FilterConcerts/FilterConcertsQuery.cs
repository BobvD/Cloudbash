using Cloudbash.Application.Common.Exceptions;
using Cloudbash.Application.Common.Interfaces;
using Cloudbash.Domain.ReadModels;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Cloudbash.Application.Concerts.Queries.FilterConcerts
{
    public class FilterConcertsQuery : IRequest<FilterConcertsVm>
    {
        public string SearchTerm { get; set; }
        public DateTime Before { get; set; }
        public DateTime After { get; set; }

        public class FilterConcertsQueryHandler : IRequestHandler<FilterConcertsQuery, FilterConcertsVm>
        {
            private readonly IViewModelRepository<Concert> _repository;

            public FilterConcertsQueryHandler(IViewModelRepository<Concert> repository)
            {
                _repository = repository;
            }

            public async Task<FilterConcertsVm> Handle(FilterConcertsQuery request, CancellationToken cancellationToken)
            {
                if(request.Before != default(DateTime) 
                    && request.After != default(DateTime) 
                    && request.Before < request.After)
                {
                    throw new ValidationException();
                }

                var children = new[] { "Venue" };

                var concerts = await _repository.FilterAsync(
                    c => c.Name.ToLower().Contains(request.SearchTerm.ToLower()) 
                    || c.Venue.Name.ToLower().Contains(request.SearchTerm.ToLower()),
                    children);
                
                if (request.After != default(DateTime))
                {
                    concerts = concerts.Where(c => c.StartDate > request.After).ToList();
                }

                if (request.Before != default(DateTime))
                {
                    concerts = concerts.Where(c => c.StartDate < request.Before).ToList();
                }

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

