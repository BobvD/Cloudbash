using Cloudbash.Application.Common.Interfaces;
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
            private readonly IApplicationDbContext _context;

            public GetProductsQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<GetConcertsVm> Handle(GetConcertsQuery request, CancellationToken cancellationToken)
            {
                var concerts = await _context.Concerts
                    .OrderBy(t => t.Name)
                    .ToListAsync(cancellationToken);

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
