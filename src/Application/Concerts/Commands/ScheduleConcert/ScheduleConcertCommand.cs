using Cloudbash.Application.Common.Exceptions;
using Cloudbash.Application.Common.Interfaces;
using Cloudbash.Domain.Concerts;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Cloudbash.Application.Concerts.Commands.ScheduleConcert
{
    public class ScheduleConcertCommand : IRequest
    {

        public Guid ConcertId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        
        public class ScheduleConcertCommandHandler : IRequestHandler<ScheduleConcertCommand>
        {

            IRepository<Concert> _repository;

            public ScheduleConcertCommandHandler(IRepository<Concert> repository)
            {
                _repository = repository;
            }

            public async Task<Unit> Handle(ScheduleConcertCommand request, CancellationToken cancellationToken)
            {
                var concert = await _repository.GetByIdAsync(request.ConcertId);
                if (concert == null)
                {
                    throw new NotFoundException(nameof(Concert), request.ConcertId);
                }
                
                concert.Schedule(request.StartDate, request.EndDate);

                await _repository.SaveAsync(concert);

                return Unit.Value;

            }
        }

    }
}
