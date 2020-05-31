using Cloudbash.Application.Common.Interfaces;
using Cloudbash.Domain.ReadModels;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Cloudbash.Application.Concerts.Queries.GetConcert
{
    public class GetConcertDetailQuery : IRequest<Concert>
    {
        public Guid Id { get; set; }

        public class GetConcertQueryHandler : IRequestHandler<GetConcertDetailQuery, Concert>
        {
            private readonly IViewModelRepository<Concert> _repository;

            public GetConcertQueryHandler(IViewModelRepository<Concert> repository)
            {
                _repository = repository;
            }

            public async Task<Concert> Handle(GetConcertDetailQuery request, CancellationToken cancellationToken)
            {
                var children = new [] { "Venue", "TicketTypes" };
                var concert = await _repository.GetAsync(request.Id, children);               

                return concert;
            }
        }
    }
}
