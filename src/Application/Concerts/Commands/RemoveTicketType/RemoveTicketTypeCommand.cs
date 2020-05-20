using Cloudbash.Application.Common.Exceptions;
using Cloudbash.Application.Common.Interfaces;
using Cloudbash.Domain.Concerts;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Cloudbash.Application.Concerts.Commands.RemoveTicketType
{
    public class RemoveTicketTypeCommand : IRequest
    {
        public Guid ConcertId { get; set; }
        public Guid TicketTypeId { get; set; }

        public class RemoveTicketTypeCommandHandler : IRequestHandler<RemoveTicketTypeCommand>
        {
            IRepository<Concert> _repository;

            public RemoveTicketTypeCommandHandler(IRepository<Concert> repository)
            {
                _repository = repository;
            }

            public async Task<Unit> Handle(RemoveTicketTypeCommand request, CancellationToken cancellationToken)
            {
                var concert = await _repository.GetByIdAsync(request.ConcertId);
                if (concert == null)
                {
                    throw new NotFoundException(nameof(Concert), request.ConcertId);
                }

                concert.RemoveTicketType(request.TicketTypeId);

                await _repository.SaveAsync(concert);

                return Unit.Value;
            }
        }
    }
}
