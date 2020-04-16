using Cloudbash.Application.Common.Exceptions;
using Cloudbash.Application.Common.Interfaces;
using Cloudbash.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Cloudbash.Application.Concerts.Commands.DeleteConcert
{
    public class DeleteConcertCommand : IRequest
    {
        public Guid Id { get; set; }

        public class DeleteConcertCommandHandler : IRequestHandler<DeleteConcertCommand>
        {
            IRepository<Concert> _repository;

            public DeleteConcertCommandHandler(IRepository<Concert> repository)
            {
                _repository = repository;
            }

            public async Task<Unit> Handle(DeleteConcertCommand request, CancellationToken cancellationToken)
            {
                Console.WriteLine("request id: " + request.Id);

                var concert = await _repository.GetByIdAsync(request.Id);
                Console.WriteLine("concert id: " + concert.Id);
                if (concert == null)
                {
                    throw new NotFoundException(nameof(Concert), request.Id);
                }
                
                concert.MarkAsDeleted();
                
                await _repository.SaveAsync(concert);
                               
                return Unit.Value;
            }
        }
    }
}